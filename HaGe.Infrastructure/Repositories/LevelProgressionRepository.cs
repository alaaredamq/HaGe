using HaGe.Core.Entities;
using HaGe.Core.Repositories;
using HaGe.Infrastructure.Context;
using HaGe.Infrastructure.Repositories.Base;

namespace HaGe.Infrastructure.Repositories; 

public class LevelProgressionRepository : Repository<LevelProgression>, ILevelProgressionRepository {
    public LevelProgressionRepository(HaGeContext dbContext)
        : base(dbContext) {
    }

    public List<Guid> GetUserLevel(Guid loggedProfileId) {
        var levelProgression = Entities.Where(x => x.ProfileId == loggedProfileId).ToList();

        if (levelProgression != null && levelProgression.Any()) {
            return levelProgression.Select(x => x.LevelId).ToList();
        }
        else {
            return new List<Guid>();
        }
    }
}