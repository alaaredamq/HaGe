using HaGe.Core.Entities;
using HaGe.Core.Repositories;
using HaGe.Infrastructure.Context;
using HaGe.Infrastructure.Repositories.Base;


namespace HaGe.Infrastructure.Repositories; 

public class RoleRepository: Repository<Role>, IRoleRepository {
    public RoleRepository(HaGeContext dbContext)
        : base(dbContext) {
    }
    
    public async Task<Role> GetDefaultRole() {
        return Table.FirstOrDefault(x => x.IsView && x.IsDefault);
    }
}