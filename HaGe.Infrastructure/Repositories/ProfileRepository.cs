using HaGe.Core.Entities;
using HaGe.Core.Repositories;
using HaGe.Infrastructure.Context;
using HaGe.Infrastructure.Repositories.Base;

namespace HaGe.Infrastructure.Repositories; 

public class ProfileRepository : Repository<Profile>, IProfileRepository {
    public ProfileRepository(HaGeContext dbContext)
        : base(dbContext) {
    }

    public int GetUserLevel(Guid profileId) {
        var profile = GetById(profileId);
        return profile.LevelUnlocked;
    }

    public Profile GetByUserId(Guid modelLoggedUserId) {
        var profile = Entities.FirstOrDefault(x => x.UserId == modelLoggedUserId);
        return profile;
    }
}