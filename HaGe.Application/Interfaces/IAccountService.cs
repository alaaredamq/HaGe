using HaGe.Application.Models;
using HaGe.Core.Entities;


namespace HaGe.Application.Interfaces {
    public interface IAccountService {
        User CreateUpdateUser(RegisterViewModel model);
        User GetUserBytId(Guid id);
        Task <Role>GetDefaultRole();
        Task<LoginViewModel> Login(User userLoginModel);
        User GetUserByEmail(string? email);
        List<User> GetUsers(int number);
    }
}

