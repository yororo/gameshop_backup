using GameShop.Data.Providers.Interfaces;
using GameShop.Data.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GameShop.Contracts.Entities;
using GameShop.Contracts.Enumerations;
using System.Data.Common;

namespace GameShop.Data.Repositories
{
    public class DummyRepository : Repository, IGameAdvertisementAsyncRepository, IGameAsyncRepository
    { 
        public DummyRepository(IDatabaseProviderClient databaseProviderClient)
            : base(databaseProviderClient)
        {
        }

        public Task<int> AddAsync(Advertisement advertisement)
        {
            throw new NotImplementedException();
        }

        public Task<int> DeleteByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<Advertisement> FindByFriendlyIdAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task<Advertisement> FindByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Advertisement>> FindByTitleAsync(string title)
        {
            throw new NotImplementedException();
        }

        public Task<User> GetAdOwnerAsync(Guid advertisementId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Advertisement>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Advertisement>> GetAllDeepAsync()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Game>> GetByGenreAsync(GameGenre genre)
        {
            throw new NotImplementedException();
        }

        public Task<Game> GetByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Game>> GetByNameAsync(string name)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Address>> GetMeetupLocationsAsync(Guid advertisementId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Game>> GetProductsAsync(Guid advertisementId)
        {
            throw new NotImplementedException();
        }

        public Task<int> UpdateAsync(Guid id, Advertisement advertisement)
        {
            throw new NotImplementedException();
        }

        Task<IEnumerable<Game>> IProductAsyncRepository<Game, Guid>.GetAllAsync()
        {
            throw new NotImplementedException();
        }
    }
}
