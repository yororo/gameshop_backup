using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

using GameShop.Contracts.Entities;
using GameShop.Data.Contracts;
using GameShop.Data.EF.Contexts;
using GameShop.Data.EF.Entities;
using GameShop.Data.EF.Entities.Games;

namespace GameShop.Data.EF.Repositories
{
    internal class GameAdvertisementRepository : IGameAdvertisementRepository
    {
        private GameShopContext _context;

        public GameAdvertisementRepository(GameShopContext context)
        {
            _context = context;
        }

        public Task<int> AddAsync(Advertisement advertisement)
        {
            var advertisementProductEntities = new List<EfAdvertisementProducts>();

            if(advertisement.Products.Count != 0)
            {
                foreach(Product productContract in advertisement.Products)
                {
                    advertisementProductEntities.Add(new EfAdvertisementProducts()
                    {
                        AdvertisementId = advertisement.Id,
                        Advertisement = advertisement.ToEntity(),
                        ProductId = productContract.Id,
                        Product = productContract.ToEntity()
                    });
                }

                _context.AdvertisementProducts.AddRange(advertisementProductEntities);

                return _context.SaveChangesAsync();
            }

            _context.Advertisements.Add(advertisement.ToEntity());

            return _context.SaveChangesAsync();
        }

        public Task<int> DeleteByIdAsync(Guid advertisementId)
        {
            _context.Entry<EfAdvertisement>(new EfAdvertisement() { Id = advertisementId }).State = EntityState.Deleted;
            
            return _context.SaveChangesAsync();
        }

        public async Task<Advertisement> FindByFriendlyIdAsync(string friendlyId)
        {
            // var advertisementEntity = await _context.Advertisements
            //             .Include(g => g.Games)
            //             .ThenInclude(sell => sell.SellingInformation)
            //             .Include(g => g.Games).ThenInclude(trade => trade.TradingInformation)
            //             .SingleOrDefaultAsync(ga => ga.FriendlyId == friendlyId);

            // TODO:
            var advertisementEntity = await _context.AdvertisementProducts
                                        .Where(ap => ap.Advertisement.FriendlyId == friendlyId)
                                        .Select(ap => ap.Advertisement)
                                        .Include(ad => ad.MeetupInformation)
                                        .ThenInclude(meetupInfo => meetupInfo.MeetupLocations)
                                        .FirstOrDefaultAsync();

            return advertisementEntity.ToContract();
        }

        public async Task<Advertisement> FindByIdAsync(Guid advertisementId)
        {
            // var advertisementEntity = await _context.Advertisements
            //             .Include(g => g.Games)
            //             .ThenInclude(sell => sell.SellingInformation)
            //             .Include(g => g.Games)
            //             .ThenInclude(trade => trade.TradingInformation)
            //             .SingleOrDefaultAsync(ga => ga.Id == advertisementId);

            var advertisementEntity = await _context.AdvertisementProducts
                                        .Include(ap => ap.Advertisement)
                                        .Select(ap => ap.Advertisement)
                                        .Include(ap => ap.AdvertisementProducts)
                                        .SingleOrDefaultAsync(ad => ad.Id == advertisementId);

            return advertisementEntity.ToContract();
        }

        public async Task<IEnumerable<Advertisement>> FindByTitleAsync(string advertisementTitle)
        {
            // var advertisementEntities = await _context.Advertisements
            //             .Include(g => g.Games)
            //             .ThenInclude(sell => sell.SellingInformation)
            //             .Include(g => g.Games)
            //             .ThenInclude(trade => trade.TradingInformation)
            //             .Where(ga => ga.Title == advertisementTitle).ToListAsync();

            var advertisementEntities = await _context.AdvertisementProducts
                                        .Include(ap => ap.Advertisement)
                                        .Where(ap => ap.Advertisement.Title == advertisementTitle)
                                        .Select(ap => ap.Advertisement)
                                        .Include(ap => ap.AdvertisementProducts)
                                        .ToListAsync();

            return advertisementEntities.ToContracts();
        }

        public Task<User> GetAdOwnerAsync(Guid advertisementId)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Advertisement>> GetAllAsync()
        {
            var advertisementEntities = await _context.Advertisements
                                        .Include(ad => ad.MeetupInformation)
                                        .ThenInclude(meetupInfo => meetupInfo.MeetupLocations)
                                        .ToListAsync();

            return advertisementEntities.ToContracts();
        }

        public async Task<IEnumerable<Advertisement>> GetAllDeepAsync()
        {
            // var advertisementEntities = await _context.Advertisements
            //             .Include(g => g.Games)
            //             .ThenInclude(sell => sell.SellingInformation)
            //             .Include(g => g.Games).ThenInclude(trade => trade.TradingInformation)
            //             .ToListAsync();

            // var advertisementEntities = await _context.Advertisements
            //                             .Include(ad => ad.MeetupInformation)
            //                             .ThenInclude(meetupInfo => meetupInfo.MeetupLocations)
            //                             .ToListAsync();

            var advertisementContracts = new List<Advertisement>();

            await _context.Advertisements
            .Include(ad => ad.MeetupInformation)
            .ThenInclude(meetupInfo => meetupInfo.MeetupLocations)
            .ForEachAsync(ad => 
            {
                // Get all advertisement products.
                var advertisementProductEntities = _context.AdvertisementProducts
                .Where(ap => ap.AdvertisementId == ad.Id)
                .Include(ap => ap.Product)
                .Include(ap => ap.Product.SellingInformation)
                .Include(ap => ap.Product.TradingInformation)
                .ToList();

                advertisementProductEntities.ForEach(ap => 
                {   
                    ad.AdvertisementProducts.Add(ap);
                });

                // Add advertisement to list.
                advertisementContracts.Add(ad.ToContract());              
            });

            return advertisementContracts;
        }

        public async Task<MeetupInformation> GetMeetupInformationAsync(Guid advertisementId)
        {
            var meetupInfoEntity = await _context.MeetupInformation
                                    .Where(meetupInfo => meetupInfo.AdvertisementId == advertisementId)
                                    .Include(meetupInfo => meetupInfo.MeetupLocations)
                                    .SingleOrDefaultAsync();

            return meetupInfoEntity.ToContract();
        }

        public async Task<IEnumerable<Product>> GetProductsAsync(Guid advertisementId)
        {
            // var gameEntities = await _context.Games
            //             .Where(g => g.AdvertisementId == advertisementId)
            //             .Include(s => s.SellingInformation)
            //             .Include(t => t.TradingInformation).ToListAsync();

            var productEntities = await _context.AdvertisementProducts
                                .Include(ap => ap.Product)
                                .Where(ap => ap.AdvertisementId == advertisementId)
                                .Select(ap => ap.Product)
                                .ToListAsync();

            return productEntities.ToContracts();
        }

        public Task<int> UpdateAsync(Guid advertisementId, Advertisement advertisement)
        {
            var advertisementEntity = advertisement.ToEntity();
            //Set ID.
            advertisementEntity.Id = advertisementId;

            _context.Entry<EfAdvertisement>(advertisementEntity).State = EntityState.Modified;

            return _context.SaveChangesAsync();
        }
    }
}