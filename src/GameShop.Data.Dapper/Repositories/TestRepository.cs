using GameShop.Data.Providers.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GameShop.Contracts.Entities;
using GameShop.Contracts.Enumerations;
using System.Data.Common;
using GameShop.Data.Contracts;

namespace GameShop.Data.Repositories
{
    /// <summary>
    /// This is just a dummy repository. This can be used for return fake database objects for testing purposes.
    /// </summary>
    public class TestRepository : Repository, IGameAdvertisementRepository, IGameRepository
    { 
        public TestRepository(IDatabaseProviderClient databaseProviderClient)
            : base(databaseProviderClient)
        {
        }

        public Task<int> AddAsync(Advertisement<Game> advertisement)
        {
            throw new NotImplementedException();
        }

        public Task<int> AddAsync(Advertisement advertisement)
        {
            throw new NotImplementedException();
        }

        public Task<int> AddGameAsync(Game game)
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

        public Task<int> UpdateAsync(Guid advertisementId, Advertisement<Game> advertisement)
        {
            throw new NotImplementedException();
        }

        public Task<int> UpdateAsync(Guid id, Advertisement advertisement)
        {
            throw new NotImplementedException();
        }

        Task<Advertisement<Game>> IAdvertisementRepository<Guid, Game>.FindByFriendlyIdAsync(string friendlyId)
        {
            throw new NotImplementedException();
        }

        Task<Advertisement<Game>> IAdvertisementRepository<Guid, Game>.FindByIdAsync(Guid advertisementId)
        {
            throw new NotImplementedException();
        }

        Task<IEnumerable<Advertisement<Game>>> IAdvertisementRepository<Guid, Game>.FindByTitleAsync(string advertisementTitle)
        {
            throw new NotImplementedException();
        }

        Task<IEnumerable<Game>> IProductRepository<Guid, Game>.GetAllAsync()
        {
            throw new NotImplementedException();
        }

        Task<IEnumerable<Advertisement<Game>>> IAdvertisementRepository<Guid, Game>.GetAllAsync()
        {
            throw new NotImplementedException();
        }

        Task<IEnumerable<Advertisement<Game>>> IAdvertisementRepository<Guid, Game>.GetAllDeepAsync()
        {
            throw new NotImplementedException();
        }
    }
}
