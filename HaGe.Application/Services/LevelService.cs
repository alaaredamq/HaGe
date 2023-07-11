using HaGe.Application.Interfaces;
using HaGe.Core.Entities;
using HaGe.Core.Repositories;

namespace HaGe.Application.Services;

public class LevelService : ILevelService {
    private readonly ILevelRepository _levelRepository;
    private readonly ILevelProgressionRepository _levelProgressionRepository;
    private readonly IProfileRepository _profileRepository;

    public LevelService(ILevelRepository levelRepository, IProfileRepository profileRepository, ILevelProgressionRepository levelProgressionRepository) {
        _levelRepository = levelRepository;
        _profileRepository = profileRepository;
        _levelProgressionRepository = levelProgressionRepository;
    }

    public int UnlockNextLevel(Guid profileId) {
        var profile = _profileRepository.GetById(profileId);
        if (profile == null) return -1;

        profile.LevelUnlocked++;
        _profileRepository.Save(profile);
        return profile.LevelUnlocked;
    }

    public Level GetByOrder(int modelLevel) {
        var level = _levelRepository.GetByOrder(modelLevel);
        return level;
    }

    public List<Level> GetUserLevel(Guid loggedProfileId) {
        var levelProgressionsIds = _levelProgressionRepository.GetUserLevel(loggedProfileId);
        var levels = _levelRepository.GetLevels(levelProgressionsIds);
        
        return levels;
    }
    
    public List<Level> GetAllLevels() {
        var levels = _levelRepository.GetAllLevels();
        return levels;
    }

    public Level GetById(Guid id) {
        return _levelRepository.GetById(id);
    }
}