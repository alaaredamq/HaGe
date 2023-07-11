using HaGe.Core.Entities;
using HaGe.Core.Repositories;
using HaGe.Infrastructure.Context;
using HaGe.Infrastructure.Repositories.Base;

namespace HaGe.Infrastructure.Repositories; 

public class LevelRepository: Repository<Level>, ILevelRepository {
    public LevelRepository(HaGeContext dbContext)
        : base(dbContext) {
    }

    public Level GetByOrder(int modelLevel) {
        var level = Entities.FirstOrDefault(x => x.Order == modelLevel);
        return level;
    }
    
    public List<Level> GetLevels(List<Guid> levelProgressionsIds) {
        var levels = Entities.Where(x => levelProgressionsIds.Contains(x.Id)).ToList();
        return levels != null && levels.Any() ? levels : new List<Level>();
    }
    
    public List<Level> GetAllLevels() {
        var levels = Entities.ToList();
        return levels != null && levels.Any() ? levels : new List<Level>();
    }
    
}