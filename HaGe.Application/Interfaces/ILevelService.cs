using HaGe.Core.Entities;

namespace HaGe.Application.Interfaces; 

public interface ILevelService {
    int UnlockNextLevel(Guid profileId);
    Level GetByOrder(int modelLevel);
    List<Level> GetUserLevel(Guid loggedProfileId);
    List<Level> GetAllLevels();
    Level GetById(Guid id);
}