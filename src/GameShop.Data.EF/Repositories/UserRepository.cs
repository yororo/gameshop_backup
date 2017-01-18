//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using GameShop.Contracts.Entities;
//using Microsoft.EntityFrameworkCore;
//using GameShop.Data.EF.Contexts;
//using GameShop.Data.EF.Translators;
//using GameShop.Data.Contracts;
//using EFEntities = GameShop.Data.EF.Entities;

//namespace GameShop.Data.EF.Repositories
//{
//    internal class UserRepository : IUserRepository
//    {
//        private GameShopContext _context;

//        public UserRepository(GameShopContext context)
//        {
//            _context = context;
//        }

//        public async Task<int> AddUserAsync(User user)
//        {
//            EFEntities.User model = user.ToUserEntity();

//            await _context.Users.AddAsync(model);
//            //await _context.Accounts.AddAsync(model.Account);
//            await _context.Profiles.AddAsync(model.Profile);
//            await _context.ProfileAddresses.AddRangeAsync(model.Profile.Addresses);
//            await _context.ProfileContactInformation.AddRangeAsync(model.Profile.ContactInformation);

//            return await _context.SaveChangesAsync();
//        }

//        public async Task<User> FindUserByEmailAsync(string email)
//        {
//            EFEntities.User model = await _context.Users.Include(u => u.Profile)
//                                                        .Include(u => u.Profile.Addresses)
//                                                        .Include(u => u.Profile.ContactInformation)
//                                                        .SingleOrDefaultAsync<EFEntities.User>(u => u.Email == email);

//            if(model == null)
//            {
//                return null;
//            }

//            return model.ToUserContract();
//        }

//        public async Task<User> FindUserByIdAsync(Guid userId)
//        {
//            EFEntities.User model = await _context.Users.Include(u => u.Profile)
//                                                        .Include(u => u.Profile.Addresses)
//                                                        .Include(u => u.Profile.ContactInformation)
//                                                        .SingleOrDefaultAsync<EFEntities.User>(user => user.Id == userId);
            
//            return model.ToUserContract();
//        }

//        public async Task<User> FindUserByUsernameAsync(string username)
//        {
//            EFEntities.User model = await _context.Users.Include(u => u.Profile)
//                                                        .Include(u => u.Profile.Addresses)
//                                                        .Include(u => u.Profile.ContactInformation)
//                                                        .SingleOrDefaultAsync<EFEntities.User>(u => u.UserName == username);

//            return model.ToUserContract();
//        }

//        public async Task<Account> FindAccountByUserIdAsync(Guid userId)
//        {
//            EFEntities.User model = await _context.Users.SingleOrDefaultAsync<EFEntities.User>(user => user.Id == userId);

//            Account account = new Account();
//            // Populate.

//            return account;
//        }

//        public async Task<Profile> FindProfileByUserIdAsync(Guid userId)
//        {
//            EFEntities.Profile model = await _context.Profiles.Include(p => p.Addresses)
//                                                                .Include(p => p.ContactInformation)
//                                                                .Include(p => p.User)
//                                                                .SingleOrDefaultAsync(p => p.UserId == userId);

//            return model.ToProfileContract();
//        }

//        public async Task<IEnumerable<User>> GetAllUsersAsync()
//        {
//            IEnumerable<EFEntities.User> userModels = await _context.Users.Include(u => u.Profile)
//                                                                            .Include(u => u.Profile.Addresses)
//                                                                            .Include(u => u.Profile.ContactInformation)
//                                                                            .ToListAsync();

//            List<User> users = new List<User>();

//            foreach (EFEntities.User model in userModels)
//            {
//                users.Add(model.ToUserContract());
//            }

//            return users;
//        }

//        public async Task<Account> GetUserAccountAsync(User user)
//        {
//            EFEntities.User model = await _context.Users.SingleOrDefaultAsync(u => u.Id == user.UserId);

//            Account account = new Account();

//            return account;
//        }

//        public async Task<Profile> GetUserProfileAsync(User user)
//        {
//            EFEntities.Profile model = await _context.Profiles.Include(p => p.Addresses)
//                                                                .Include(p => p.ContactInformation)
//                                                                .Include(p => p.User)
//                                                                .SingleOrDefaultAsync(p => p.UserId == user.UserId);

//            return model.ToProfileContract();
//        }

//        public async Task<int> UpdateUserAsync(Guid userId, User updatedUser)
//        {
//            EFEntities.User userModelToUpdate = await _context.Users.SingleOrDefaultAsync(user => user.Id == userId);
//            EFEntities.User updatedUserModel = updatedUser.ToUserEntity();

//            //userModelToUpdate.Account = updatedUserModel.Account;
//            userModelToUpdate.Profile = updatedUserModel.Profile;

//            return await _context.SaveChangesAsync();
//        }

//        public async Task<int> DeleteUserAsync(User user)
//        {
//            _context.Users.Remove(user.ToUserEntity());

//            return await _context.SaveChangesAsync();
//        }

//        public async Task<int> DeleteUserByIdAsync(Guid userId)
//        {
//            EFEntities.User user = await _context.Users.SingleOrDefaultAsync(u => u.Id == userId);

//            _context.Users.Remove(user);

//            return await _context.SaveChangesAsync();
//        }

//        public Task<int> AddUserAsync(User user, string password)
//        {
//            throw new NotImplementedException();
//        }

//        public Task<int> UpdateUserAsync(User updatedUser)
//        {
//            throw new NotImplementedException();
//        }

//        public Task<int> UpdateUserAccountAsync(Guid userId, Account account)
//        {
//            throw new NotImplementedException();
//        }

//        public Task<int> UpdateUserProfileAsync(Guid userId, Profile profile)
//        {
//            throw new NotImplementedException();
//        }

//        public Task<int> ChangeUserPassword(User user, string oldPassword, string newPassword)
//        {
//            throw new NotImplementedException();
//        }
//    }
//}
