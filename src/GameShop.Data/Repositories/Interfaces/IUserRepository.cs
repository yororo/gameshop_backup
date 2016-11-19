using GameShop.Contracts.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameShop.Data.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task<User> FindUserById(Guid userId);
        Task<User> FindUserByUsername(string username);
        Task<User> FindUserByEmail(string email);
        Task<IEnumerable<User>> GetAllUsersAsync();
        Task<Account> GetUserAccountAsync(User user);
        Task<Account> GetAccountByUserIdAsync(Guid userId);
        Task<Profile> GetUserProfileAsync(User user);
        Task<Profile> GetProfileByUserIdAsync(Guid userId);

        Task AddUserAsync(User user);
        Task<User> UpdateUserAsync(Guid userId, User updatedUser);
        Task DeleteUserByIdAsync(Guid userId);
    }
}
