using HaGe.Core.Entities;
using HaGe.Core.Repositories;
using HaGe.Infrastructure.Context;
using HaGe.Infrastructure.Repositories.Base;

namespace HaGe.Infrastructure.Repositories; 

public class LevelProgressionRepository : Repository<LevelProgression>, ILevelProgressionRepository {
    public LevelProgressionRepository(HaGeContext dbContext)
        : base(dbContext) {
    }   
}