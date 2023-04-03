using HaGe.Core.Entities;
using HaGe.Core.Repositories.Base;

namespace HaGe.Core.Repositories; 

public interface IRoleRepository : IRepository<Role> {
    Task<Role> GetDefaultRole();
}