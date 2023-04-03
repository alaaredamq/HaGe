using HaGe.Core.Entities;
using HaGe.Core.Repositories.Base;

namespace HaGe.Core.Repositories; 

public interface IAccountRepository : IRepository<User> {
    Task<User> Login(User user);
    User GetUserByEmail(string? email);
    Task<User> GetUserByEmailAsync(string? email);
}