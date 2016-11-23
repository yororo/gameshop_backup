using GameShop.Contracts.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameShop.Data.Contracts
{
    public interface IUserRepository
    {
        Task<User> FindUserById(Guid userId);
        Task<User> FindUserByUsername(string username);
        Task<User> FindUserByEmail(string email);
        Task<Account> FindAccountByUserIdAsync(Guid userId);
        Task<Profile> FindProfileByUserIdAsync(Guid userId);
        Task<IEnumerable<User>> GetAllUsersAsync();
        Task<Account> GetUserAccountAsync(User user);
        Task<Profile> GetUserProfileAsync(User user);

        Task<int> AddUserAsync(User user);
        Task<int> UpdateUserAsync(Guid userId, User updatedUser);
        Task<int> DeleteUserByIdAsync(Guid userId);
        Task<int> DeleteUserAsync(User user);
    }
}
