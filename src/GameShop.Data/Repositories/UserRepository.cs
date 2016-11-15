using GameShop.Data.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GameShop.Contracts.Entities;
using GameShop.Data.Providers.Interfaces;

using Dapper;
using GameShop.Data.Translators;
using System.Data;

namespace GameShop.Data.Repositories
{
    public class UserRepository : Repository, IUserRepository
    {
        public UserRepository(IDatabaseProviderClient databaseProviderClient) :
            base(databaseProviderClient)
        {

        }

        public async Task<User> FindUserById(Guid userId)
        {
            string commandText = "SELECT * FROM Users u, Profiles p, Accounts a WHERE u.UserId LIKE @userId AND u.ProfileId LIKE p.ProfileId AND u.AccountId LIKE a.AccountId";

            using (var databaseConnection = Client.CreateConnection())
            {
                //Create command.
                CommandDefinition command = new CommandDefinition(commandText, new { @userId = userId });

                // Query database.
                dynamic userData = await databaseConnection.QuerySingleOrDefaultAsync(command);
                
                // Translate user data.
                User user = DynamicDataTranslator.TranslateUser(userData);

                return user;
            }
        }

        public async Task<User> FindUserByUsername(string username)
        {
            string commandText = "SELECT * FROM Users u, Profiles p, Accounts a WHERE a.Username LIKE @username AND u.ProfileId LIKE p.ProfileId AND u.AccountId LIKE a.AccountId";

            using (var databaseConnection = Client.CreateConnection())
            {
                //Create command.
                CommandDefinition command = new CommandDefinition(commandText, new { @username = username });

                // Query database.
                dynamic userData = await databaseConnection.QuerySingleOrDefaultAsync(command);

                // Translate user data.
                User user = DynamicDataTranslator.TranslateUser(userData);

                return user;
            }
        }

        public async Task<User> FindUserByEmail(string email)
        {
            string commandText = "SELECT * FROM Users u, Profiles p, Accounts a WHERE a.Email LIKE @email AND u.AccountId LIKE a.AccountId AND u.ProfileId LIKE p.ProfileId";

            using (var databaseConnection = Client.CreateConnection())
            {
                //Create command.
                CommandDefinition command = new CommandDefinition(commandText, new { @email = email });

                // Query database.
                dynamic userData = await databaseConnection.QuerySingleOrDefaultAsync(command);

                // Translate user data.
                User user = DynamicDataTranslator.TranslateUser(userData);

                return user;
            }
        }

        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            var commandText = "SELECT * FROM Users u, Profiles p, Accounts a WHERE u.ProfileId LIKE p.ProfileId AND u.AccountId LIKE a.AccountId";

            using (var databaseConnection = Client.CreateConnection())
            {
                // Query database.
                IEnumerable<dynamic> usersData = await databaseConnection.QueryAsync(commandText);

                // Initialize list.
                List<User> users = new List<User>();

                foreach(dynamic userData in usersData)
                {
                    // Translate user data.
                    User user = DynamicDataTranslator.TranslateUser(userData);

                    users.Add(user);
                }

                return users;
            }
        }

        public async Task<Account> GetUserAccountAsync(User user)
        {
            return await GetUserAccountByIdAsync(user.UserId);
        }

        public async Task<Account> GetUserAccountByIdAsync(Guid userId)
        {
            var commandText = "SELECT * FROM Users u, Accounts a WHEREu.UserId LIKE @userId AND u.AccountId LIKE a.AccountId";

            using (var databaseConnection = Client.CreateConnection())
            {
                // Create command.
                CommandDefinition command = new CommandDefinition(commandText, new { @userId = userId });

                // Query database.
                dynamic accountData = await databaseConnection.QueryAsync(command);

                // Translate account data.
                Account account = DynamicDataTranslator.TranslateAccount(accountData);

                return account;
            }
        }

        public async Task<Profile> GetUserProfileAsync(User user)
        {
            return await GetUserProfileByIdAsync(user.UserId);
        }

        public async Task<Profile> GetUserProfileByIdAsync(Guid userId)
        {
            var commandText = "SELECT * FROM Users u, Profiles p WHERE u.UserId LIKE @userId AND u.ProfileId LIKE p.ProfileId";

            using (var databaseConnection = Client.CreateConnection())
            {
                // Create command.
                CommandDefinition command = new CommandDefinition(commandText, new { @userId = userId });

                // Query database.
                dynamic profileData = await databaseConnection.QueryAsync(command);

                // Translate profile data.
                Profile profile = DynamicDataTranslator.TranslateProfile(profileData);

                return profile;
            }
        }

        public async Task AddUserAsync(User user)
        {
            var insertToProfilesTable = @"INSERT INTO [dbo].[Profiles]
                                               ([ProfileId]
                                               ,[Salutation]
                                               ,[FirstName]
                                               ,[MiddleName]
                                               ,[LastName]
                                               ,[Birthday]
                                               ,[Gender]
                                               ,[CivilStatus]
                                               ,[CreatedDate]
                                               ,[ModifiedDate])
                                         VALUES (@profileId, @salutation, @firstName, @middleName, @lastName, @birthday, @gender, @civilStatus, @createdDate, @modifiedDate)";

            var insertToAccountsTable = @"INSERT INTO [dbo].[Accounts]
                                               ([AccountId]
                                               ,[IsActive]
                                               ,[Username]
                                               ,[Email]
                                               ,[EmailConfirmed]
                                               ,[LockoutEnabled]
                                               ,[LockoutEnd]
                                               ,[PasswordHash]
                                               ,[PhoneNumber]
                                               ,[PhoneNumberConfirmed]
                                               ,[AccessFailedCount]
                                               ,[ConcurrencyStamp]
                                               ,[SecurityStamp]
                                               ,[SecurityQuestion]
                                               ,[SecurityAnswer]
                                               ,[CreatedDate]
                                               ,[ModifiedDate])
                                         VALUES (@accountId, @isActive, @username, @email, @emailConfirmed, @lockoutEnabled, @lockoutEnd, @passwordHash, @phoneNumber, @phoneNumberConfirmed, 
                                                    @accessfailedCount, @concurrencyStamp, @securityStamp, @securityQuestion, @securityAnswer, @createdDate, @modifiedDate)";

            var insertToUsersTable = @"INSERT INTO [dbo].[Users]
                                            ([UserId]
                                            ,[ProfileId]
                                            ,[AccountId]
                                            ,[CreatedDate]
                                            ,[ModifiedDate]) 
                                        VALUES (@userId, @profileId, @accountId, @createdDate, @modifiedDate)";

            using (var databaseConnection = Client.CreateConnection())
            {
                await databaseConnection.OpenAsync();

                // Begin transaction.
                using (IDbTransaction transaction = databaseConnection.BeginTransaction())
                {
                    try
                    {
                        // Generate new IDs.
                        user.UserId = Guid.NewGuid();
                        user.Profile.ProfileId = Guid.NewGuid();
                        user.Account.AccountId = Guid.NewGuid();

                        CommandDefinition insertToProfilesCommand = new CommandDefinition(insertToProfilesTable, new
                        {
                            @profileId = user.Profile.ProfileId,
                            @salutation = user.Profile.Name.Salutation,
                            @firstName = user.Profile.Name.FirstName,
                            @middleName = user.Profile.Name.MiddleName,
                            @lastName = user.Profile.Name.LastName,
                            @birthday = user.Profile.Birthday,
                            @gender = user.Profile.Gender,
                            @civilStatus = user.Profile.CivilStatus,
                            @createdDate = DateTime.Now,
                            @modifiedDate = DateTime.Now
                        }, transaction);

                        CommandDefinition insertToAccountsCommand = new CommandDefinition(insertToAccountsTable, new
                        {
                            @accountId = user.Account.AccountId,
                            @isActive = user.Account.IsActive,
                            @username = user.Account.Username,
                            @email = user.Account.Email,
                            @emailConfirmed = user.Account.EmailVerified,
                            @lockoutEnabled = false,
                            @lockoutEnd = DateTimeOffset.MaxValue,
                            @passwordHash = user.Account.PasswordHash,
                            @phoneNumber = "09168882716",
                            @phoneNumberConfirmed = false,
                            @accessfailedCount = 0,
                            @concurrencyStamp = "123",
                            @securityStamp = "123",
                            @securityQuestion = "SecQ",
                            @securityAnswer = "SecA",
                            @createdDate = DateTime.Now,
                            @modifiedDate = DateTime.Now
                        }, transaction);

                        CommandDefinition insertToUsersCommand = new CommandDefinition(insertToUsersTable, new
                        {
                            @userId = user.UserId,
                            @profileId = user.Profile.ProfileId,
                            @accountId = user.Account.AccountId,
                            @createdDate = DateTime.Now,
                            @modifiedDate = DateTime.Now
                        }, transaction);

                        Task x = databaseConnection.ExecuteAsync(insertToProfilesCommand);
                        Task y = databaseConnection.ExecuteAsync(insertToAccountsCommand);
                        Task z = databaseConnection.ExecuteAsync(insertToUsersCommand);

                        await Task.WhenAll(x, y, z);

                        transaction.Commit();
                    }
                    catch(Exception ex)
                    {
                        // Rollback what's been inserted.
                        transaction.Rollback();

                        throw new Exception("Unable to insert user to the database.", ex);
                    }
                }
            }
        }

        public Task<User> UpdateUserAsync(Guid userId, User updatedUser)
        {
            throw new NotImplementedException();
        }

        public Task DeleteUserByIdAsync(Guid userId)
        {
            throw new NotImplementedException();
        }
    }
}
