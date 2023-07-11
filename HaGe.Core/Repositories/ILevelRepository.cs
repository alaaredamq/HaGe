using HaGe.Core.Entities;
using HaGe.Core.Repositories.Base;

namespace HaGe.Core.Repositories; 

public interface ILevelRepository : IRepository<Level> {
    Level GetByOrder(int modelLevel);
    List<Level> GetLevels(List<Guid> levelProgressionsIds);
    List<Level> GetAllLevels();
}