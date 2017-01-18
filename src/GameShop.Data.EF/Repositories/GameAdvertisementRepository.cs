using GameShop.Data.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GameShop.Contracts.Entities;

namespace GameShop.Data.EF.Repositories
{
    internal class GameAdvertisementRepository : IGameAdvertisementRepository
    {
        public Task<int> AddAsync(Advertisement<Game> advertisement)
        {
            throw new NotImplementedException();
        }

        public Task<int> DeleteByIdAsync(Guid advertisementId)
        {
            throw new NotImplementedException();
        }

        public Task<Advertisement<Game>> FindByFriendlyIdAsync(string friendlyId)
        {
            throw new NotImplementedException();
        }

        public Task<Advertisement<Game>> FindByIdAsync(Guid advertisementId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Advertisement<Game>>> FindByTitleAsync(string advertisementTitle)
        {
            throw new NotImplementedException();
        }

        public Task<User> GetAdOwnerAsync(Guid advertisementId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Advertisement<Game>>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Advertisement<Game>>> GetAllDeepAsync()
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
    }
}
