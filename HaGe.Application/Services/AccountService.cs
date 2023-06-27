using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HaGe.Application.Interfaces;
using HaGe.Application.Mapper;
using HaGe.Application.Models;
using HaGe.Core.Entities;
using HaGe.Core.Repositories;

namespace HaGe.Application.Services {
    public class AccountService : IAccountService {

        private readonly IAccountRepository _accountRepository;
        private readonly IRoleRepository _roleRepository;
        
        public AccountService(IAccountRepository accountRepository, IRoleRepository roleRepository) {
            _accountRepository = accountRepository;
            _roleRepository = roleRepository;
        }
        public User CreateUpdateUser(RegisterViewModel model) {
            var user = ObjectMapper.Mapper.Map<User>(model);
            var request = _accountRepository.SaveAsync(user);
            request.Wait();
            if (request.IsCompletedSuccessfully) {
                return request.Result;
            }

            return null!;
        }

        public User GetUserBytId(Guid id) {
            var account = _accountRepository.GetByIdAsync(id);
            if (account.IsCompletedSuccessfully) {
                return account.Result;
            }

            return null!;
        }
        
        public Task<Role> GetDefaultRole()
        {
            var defaultRole = _roleRepository.GetDefaultRole();
            if (defaultRole != null)
            {
                return defaultRole;
            }
            return null;
        }

        public Task<LoginViewModel> Login(User userLoginModel) {
            var userLoginModelResult = _accountRepository.Login(userLoginModel).Result;
            if (userLoginModel == null) return Task.FromResult<LoginViewModel>(null!);
            var retModel = ObjectMapper.Mapper.Map<LoginViewModel>(userLoginModelResult);
            return Task.FromResult(retModel);
        }
        
        public User GetUserByEmail(string? email) {
            var account = _accountRepository.GetUserByEmailAsync(email);
            if (account.IsCompletedSuccessfully) {
                return account.Result;
            }

            return null!;
        }

        public List<User> GetUsers(int number) {
            return _accountRepository.Table.Take(number).ToList();
        }
    }
}

