using HaGe.Core.Entities;
using HaGe.Core.Repositories;
using HaGe.Infrastructure.Context;
using HaGe.Infrastructure.Repositories.Base;

namespace HaGe.Infrastructure.Repositories; 

public class ProfileRepository : Repository<Profile>, IProfileRepository {
    public ProfileRepository(HaGeContext dbContext)
        : base(dbContext) {
    }    
}