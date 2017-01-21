using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

using GameShop.Contracts.Entities;
using GameShop.Data.Contracts;
using GameShop.Data.EF.Contexts;
using GameShop.Data.EF.Translators;
using GameShop.Data.EF.Translators.Products.Games;

using EFEntities = GameShop.Data.EF.Entities;

namespace GameShop.Data.EF.Repositories
{
    internal class GameAdvertisementRepository : IGameAdvertisementRepository
    {
        private GameShopContext _context;

        public GameAdvertisementRepository(GameShopContext context)
        {
            _context = context;
        }

        public async Task<int> AddAsync(Advertisement<Game> advertisement)
        {
            await _context.GameAdvertisements.AddAsync(advertisement.ToEntity());

            return await _context.SaveChangesAsync();
        }

        public Task<int> DeleteByIdAsync(Guid advertisementId)
        {
            _context.GameAdvertisements.Remove(_context.GameAdvertisements.SingleOrDefault(ad => ad.Id == advertisementId));

            return _context.SaveChangesAsync();
        }

        public async Task<Advertisement<Game>> FindByFriendlyIdAsync(string friendlyId)
        {
            var efAd = await _context.GameAdvertisements
                        .Include(g => g.Games)
                        .ThenInclude(sell => sell.SellingInformation)
                        .Include(g => g.Games).ThenInclude(trade => trade.TradingInformation)
                        .SingleOrDefaultAsync(ga => ga.FriendlyId == friendlyId);

            return efAd.ToContract();
        }

        public async Task<Advertisement<Game>> FindByIdAsync(Guid advertisementId)
        {
            var efAd = await _context.GameAdvertisements
                        .Include(g => g.Games)
                        .ThenInclude(sell => sell.SellingInformation)
                        .Include(g => g.Games)
                        .ThenInclude(trade => trade.TradingInformation)
                        .SingleOrDefaultAsync(ga => ga.Id == advertisementId);

            return efAd.ToContract();
        }

        public async Task<IEnumerable<Advertisement<Game>>> FindByTitleAsync(string advertisementTitle)
        {
            var gameAds = new List<Advertisement<Game>>();

            var eFAds = await _context.GameAdvertisements
                        .Include(g => g.Games)
                        .ThenInclude(sell => sell.SellingInformation)
                        .Include(g => g.Games)
                        .ThenInclude(trade => trade.TradingInformation)
                        .Where(ga => ga.Title == advertisementTitle).ToListAsync();

            foreach(EFEntities.Games.GameAdvertisement efAd in eFAds)
            {
                gameAds.Add(efAd.ToContract());
            }

            return gameAds;
        }

        public Task<User> GetAdOwnerAsync(Guid advertisementId)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Advertisement<Game>>> GetAllAsync()
        {
            var gameAds = new List<Advertisement<Game>>();

            var eFAds = await _context.GameAdvertisements
                        .Include(g => g.Games)
                        .ToListAsync();

            foreach(EFEntities.Games.GameAdvertisement efAd in eFAds)
            {
                gameAds.Add(efAd.ToContract());
            }

            return gameAds;
        }

        public async Task<IEnumerable<Advertisement<Game>>> GetAllDeepAsync()
        {
            var gameAds = new List<Advertisement<Game>>();

            var eFAds = await _context.GameAdvertisements
                        .Include(g => g.Games)
                        .ThenInclude(sell => sell.SellingInformation)
                        .Include(g => g.Games).ThenInclude(trade => trade.TradingInformation)
                        .ToListAsync();

            foreach(EFEntities.Games.GameAdvertisement efAd in eFAds)
            {
                gameAds.Add(efAd.ToContract());
            }

            return gameAds;
        }

        public Task<IEnumerable<Address>> GetMeetupLocationsAsync(Guid advertisementId)
        {
            // TODO: No MeetupLocation table yet.
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Game>> GetProductsAsync(Guid advertisementId)
        {
            var games = new List<Game>();

            var efGames = await _context.Games
                        .Where(g => g.AdvertisementId == advertisementId)
                        .Include(s => s.SellingInformation)
                        .Include(t => t.TradingInformation).ToListAsync();

            foreach(EFEntities.Games.Game efGame in efGames)
            {
                games.Add(efGame.ToContract());
            }

            return games;
        }

        public Task<int> UpdateAsync(Guid advertisementId, Advertisement<Game> advertisement)
        {
            EFEntities.Games.GameAdvertisement efAdFromDB = (EFEntities.Games.GameAdvertisement) _context.GameAdvertisements
                        .Where(ga => ga.Id == advertisementId);
            
            EFEntities.Games.GameAdvertisement efAdToUpdate = advertisement.ToEntity();

            efAdFromDB.CreatedDate = efAdToUpdate.CreatedDate;
            efAdFromDB.Description = efAdToUpdate.Description;
            efAdFromDB.FriendlyId = efAdToUpdate.FriendlyId;

            foreach(EFEntities.Games.Game game in efAdToUpdate.Games)
            {
                efAdFromDB.Games.Add(game);
            }

            efAdFromDB.Id = efAdToUpdate.Id;
            //fGame.MeetupInformation = contractAdGame.MeetupInformation;
            efAdFromDB.ModifiedDate = efAdToUpdate.ModifiedDate;
            //efGame.Owner = contractAdGame.Owner;
            efAdFromDB.State = efAdToUpdate.State;
            efAdFromDB.Title = efAdToUpdate.Title;

            _context.GameAdvertisements.Update(efAdFromDB);

            return _context.SaveChangesAsync();
        }
    }
}