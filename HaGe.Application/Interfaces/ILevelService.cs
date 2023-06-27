using HaGe.Core.Entities;

namespace HaGe.Application.Interfaces; 

public interface ILevelService {
    int UnlockNextLevel(Guid profileId);
    Level GetByOrder(int modelLevel);
}