using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GameShop.Contracts.Entities;
using Microsoft.EntityFrameworkCore;
using GameShop.Data.EF.Contexts;
using GameShop.Data.EF.Translators;
using GameShop.Data.Contracts;
using EFEntities = GameShop.Data.EF.Entities;

namespace GameShop.Data.EF.Repositories
{
    public class UserRepository : IUserRepository
    {
        private GameShopContext _context;

        public UserRepository(GameShopContext context)
        {
            _context = context;
        }

        public async Task<int> AddUserAsync(User user)
        {
            EFEntities.User model = user.ToUserEntity();

            await _context.Users.AddAsync(model);
            await _context.Accounts.AddAsync(model.Account);
            await _context.Profiles.AddAsync(model.Profile);
            await _context.ProfileAddresses.AddRangeAsync(model.Profile.Addresses);
            await _context.ProfileContactInformation.AddRangeAsync(model.Profile.ContactInformation);

            return await _context.SaveChangesAsync();
        }

        public async Task<User> FindUserByEmail(string email)
        {
            EFEntities.User model = await _context.Users.Include(u => u.Account)
                                                        .Include(u => u.Profile)
                                                        .Include(u => u.Profile.Addresses)
                                                        .Include(u => u.Profile.ContactInformation)
                                                        .SingleOrDefaultAsync<EFEntities.User>(u => u.Account.Email == email);

            if(model == null)
            {
                return null;
            }

            return model.ToUserContract();
        }

        public async Task<User> FindUserById(Guid userId)
        {
            EFEntities.User model = await _context.Users.Include(u => u.Account)
                                                        .Include(u => u.Profile)
                                                        .Include(u => u.Profile.Addresses)
                                                        .Include(u => u.Profile.ContactInformation)
                                                        .SingleOrDefaultAsync<EFEntities.User>(u => u.UserId == userId);
            
            return model.ToUserContract();
        }

        public async Task<User> FindUserByUsername(string username)
        {
            EFEntities.User model = await _context.Users.Include(u => u.Account)
                                                        .Include(u => u.Profile)
                                                        .Include(u => u.Profile.Addresses)
                                                        .Include(u => u.Profile.ContactInformation)
                                                        .SingleOrDefaultAsync<EFEntities.User>(u => u.Account.Username == username);

            return model.ToUserContract();
        }

        public async Task<Account> FindAccountByUserIdAsync(Guid userId)
        {
            EFEntities.Account model = await _context.Accounts.SingleOrDefaultAsync<EFEntities.Account>(a => a.UserId == userId);

            return model.ToAccountContract();
        }

        public async Task<Profile> FindProfileByUserIdAsync(Guid userId)
        {
            EFEntities.Profile model = await _context.Profiles.Include(p => p.Addresses)
                                                                .Include(p => p.ContactInformation)
                                                                .Include(p => p.User)
                                                                .SingleOrDefaultAsync(p => p.UserId == userId);

            return model.ToProfileContract();
        }

        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            IEnumerable<EFEntities.User> userModels = await _context.Users.Include(u => u.Account)
                                                                            .Include(u => u.Profile)
                                                                            .Include(u => u.Profile.Addresses)
                                                                            .Include(u => u.Profile.ContactInformation)
                                                                            .ToListAsync();

            List<User> users = new List<User>();

            foreach (EFEntities.User model in userModels)
            {
                users.Add(model.ToUserContract());
            }

            return users;
        }

        public async Task<Account> GetUserAccountAsync(User user)
        {
            EFEntities.Account model = await _context.Accounts.SingleOrDefaultAsync(p => p.UserId == user.UserId);

            return model.ToAccountContract();
        }

        public async Task<Profile> GetUserProfileAsync(User user)
        {
            EFEntities.Profile model = await _context.Profiles.Include(p => p.Addresses)
                                                                .Include(p => p.ContactInformation)
                                                                .Include(p => p.User)
                                                                .SingleOrDefaultAsync(p => p.UserId == user.UserId);

            return model.ToProfileContract();
        }

        public async Task<int> UpdateUserAsync(Guid userId, User updatedUser)
        {
            EFEntities.User userModelToUpdate = await _context.Users.SingleOrDefaultAsync(u => u.UserId == userId);
            EFEntities.User updatedUserModel = updatedUser.ToUserEntity();

            userModelToUpdate.Account = updatedUserModel.Account;
            userModelToUpdate.Profile = updatedUserModel.Profile;
            userModelToUpdate.ModifiedDate = updatedUserModel.ModifiedDate;

            return await _context.SaveChangesAsync();
        }

        public async Task<int> DeleteUserAsync(User user)
        {
            _context.Users.Remove(user.ToUserEntity());

            return await _context.SaveChangesAsync();
        }

        public async Task<int> DeleteUserByIdAsync(Guid userId)
        {
            EFEntities.User user = await _context.Users.SingleOrDefaultAsync(u => u.UserId == userId);

            _context.Users.Remove(user);

            return await _context.SaveChangesAsync();
        }
    }
}
