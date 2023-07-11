using HaGe.Core.Entities;
using HaGe.Core.Repositories.Base;

namespace HaGe.Core.Repositories; 

public interface ILevelProgressionRepository : IRepository<LevelProgression> {
    List<Guid> GetUserLevel(Guid loggedProfileId);
}