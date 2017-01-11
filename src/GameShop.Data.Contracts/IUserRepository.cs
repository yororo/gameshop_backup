using GameShop.Contracts.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameShop.Data.Contracts
{
    public interface IUserRepository
    {
        Task<User> FindUserByIdAsync(Guid userId);
        Task<User> FindUserByUsernameAsync(string username);
        Task<User> FindUserByEmailAsync(string email);
        Task<Account> FindAccountByUserIdAsync(Guid userId);
        Task<Profile> FindProfileByUserIdAsync(Guid userId);
        Task<IEnumerable<User>> GetAllUsersAsync();
        Task<Account> GetUserAccountAsync(User user);
        Task<Profile> GetUserProfileAsync(User user);

        Task<int> AddUserAsync(User user);
        Task<int> AddUserAsync(User user, string password);
        Task<int> UpdateUserAsync(User updatedUser);
        Task<int> UpdateUserAccountAsync(Guid userId, Account account);
        Task<int> UpdateUserProfileAsync(Guid userId, Profile profile);
        Task<int> DeleteUserByIdAsync(Guid userId);
        Task<int> DeleteUserAsync(User user);

        Task<int> ChangeUserPassword(User user, string oldPassword, string newPassword);
    }
}
