using GameShop.Data.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GameShop.Contracts.Entities;
using GameShop.Data.EF.Contexts;
using GameShop.Data.EF.Translators;
using EFEntities = GameShop.Data.EF.Entities;
using Microsoft.EntityFrameworkCore;

namespace GameShop.Data.EF.Repositories
{
    internal class GameAdvertisementRepository : IGameAdvertisementRepository
    {
        private GameShopContext _context;

        public GameAdvertisementRepository(GameShopContext context)
        {
            _context = context;
        }

        public Task<int> AddAsync(Advertisement<Game> advertisement)
        {
            throw new NotImplementedException();
        }

        public Task<int> DeleteByIdAsync(Guid advertisementId)
        {
            throw new NotImplementedException();
        }

        public async Task<Advertisement<Game>> FindByFriendlyIdAsync(string friendlyId)
        {
            var efAd = await _context.GameAdvertisements
                        .Include(g => g.Games)
                        .ThenInclude(sell => sell.SellingInformation)
                        .Include(g => g.Games).ThenInclude(trade => trade.TradingInformation)
                        .SingleOrDefaultAsync(ga => ga.FriendlyId == friendlyId);

            return efAd.ToAdvertisementContract();
        }

        public async Task<Advertisement<Game>> FindByIdAsync(Guid advertisementId)
        {
            var efAd = await _context.GameAdvertisements
                        .Include(g => g.Games)
                        .ThenInclude(sell => sell.SellingInformation)
                        .Include(g => g.Games)
                        .ThenInclude(trade => trade.TradingInformation)
                        .SingleOrDefaultAsync(ga => ga.Id == advertisementId);

            return efAd.ToAdvertisementContract();
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
                gameAds.Add(efAd.ToAdvertisementContract());
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
                gameAds.Add(efAd.ToAdvertisementContract());
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
                gameAds.Add(efAd.ToAdvertisementContract());
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
                games.Add(efGame.ToGameContract());
            }

            return games;
        }

        public Task<int> UpdateAsync(Guid advertisementId, Advertisement<Game> advertisement)
        {
            throw new NotImplementedException();
        }
    }
}