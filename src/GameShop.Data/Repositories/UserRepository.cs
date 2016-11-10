using GameShop.Data.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GameShop.Contracts.Entities;
using GameShop.Data.Providers.Interfaces;

namespace GameShop.Data.Repositories
{
    public class UserRepository : Repository, IUsersRepository
    {
        public UserRepository(IDatabaseProviderClient databaseProviderClient) :
            base(databaseProviderClient)
        { }

        public async Task<User> FindUserById(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            throw new NotImplementedException();
        }
    }
}
