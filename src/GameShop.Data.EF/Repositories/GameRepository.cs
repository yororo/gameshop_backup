using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

using GameShop.Contracts.Entities;
using GameShop.Contracts.Enumerations;
using GameShop.Data.Contracts;
using GameShop.Data.EF.Contexts;
using GameShop.Data.EF.Translators;
using GameShop.Data.EF.Translators.Products.Games;


namespace GameShop.Data.EF.Repositories
{
    internal class GameRepository : IGameRepository
    {
        private GameShopContext _context;

        public GameRepository(GameShopContext context)
        {
            _context = context;
        }

        public async Task<int> AddAsync(Game game)
        {
            await _context.Games.AddAsync(game.ToEntity());

            return await _context.SaveChangesAsync();
        }

        public async Task<int> DeleteByIdAsync(Guid productId)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Game>> GetAllAsync()
        {
            var gameEntities = await _context.Games
                                .Include(game => game.SellingInformation)
                                .Include(game => game.TradingInformation)
                                .ToListAsync();

            return gameEntities.ToContracts();
        }

        public async Task<IEnumerable<Game>> GetByGenreAsync(GameGenre genre)
        {
            var gameEntities = await _context.Games
                                .Where(game => game.GameGenre == genre)
                                .ToListAsync();

            return gameEntities.ToContracts();
        }

        public async Task<Game> GetByIdAsync(Guid id)
        {
            var gameEntity = await _context.Games
                                .SingleOrDefaultAsync(game => game.Id == id);

            return gameEntity.ToContract();
        }

        public async Task<IEnumerable<Game>> GetByNameAsync(string name)
        {
            name = name.Trim();
            
            var gameEntities = await _context.Games
                                .Where(game => game.Name.Trim() == name)
                                .ToListAsync();

            return gameEntities.ToContracts();
        }

        public async Task<int> UpdateAsync(Guid productId, Game product)
        {
            throw new NotImplementedException();
        }
    }
}