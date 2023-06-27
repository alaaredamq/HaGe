using HaGe.Application.Interfaces;
using HaGe.Core.Entities;
using HaGe.Core.Repositories;

namespace HaGe.Application.Services;

public class LevelService : ILevelService {
    private readonly ILevelRepository _levelRepository;
    private readonly IProfileRepository _profileRepository;

    public LevelService(ILevelRepository levelRepository, IProfileRepository profileRepository) {
        _levelRepository = levelRepository;
        _profileRepository = profileRepository;
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
}