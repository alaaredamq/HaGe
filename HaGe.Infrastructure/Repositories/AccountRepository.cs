using HaGe.Core.Entities;
using HaGe.Core.Repositories;
using HaGe.Infrastructure.Context;
using HaGe.Infrastructure.Repositories.Base;

namespace HaGe.Infrastructure.Repositories;

public class AccountRepository : Repository<User>, IAccountRepository {
    public AccountRepository(HaGeContext dbContext)
        : base(dbContext) {
    }

    public Task<User> Login(User user) {
        var userCheck = Table.FirstOrDefault(x => x.Username == user.Username);
        return Task.FromResult(userCheck!.Password == user.Password ? userCheck : null)!;
    }

    public User GetUserByEmail(string? email) {
        return Table.FirstOrDefault(x => x.Email == email)!;
    }

    public Task<User> GetUserByEmailAsync(string? email) {
        return Task.FromResult(Table.FirstOrDefault(x => x.Email == email)!);
    }
}