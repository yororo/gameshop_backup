using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

using GameShop.Contracts.Entities;
using GameShop.Contracts.Entities.Products;
using GameShop.Contracts.Enumerations;
using GameShop.Data.Contracts.Products;
using GameShop.Data.EF.Contexts;
using GameShop.Data.EF.Entities.Products;

namespace GameShop.Data.EF.Repositories.Products
{
    internal class GameRepository : IGameRepository
    {
        private readonly GameShopContext _context;

        public GameRepository(GameShopContext context)
        {
            _context = context;
        }

        public Task<int> AddAsync(Game game)
        {
            _context.Games.Add(game.ToEntity());

            return _context.SaveChangesAsync();
        }

        public Task<int> DeleteByIdAsync(Guid productId)
        {
            _context.Entry<EfGame>(new EfGame() { Id = productId }).State = EntityState.Deleted;

            return _context.SaveChangesAsync();
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
                                .Where(game => game.Genre == genre)
                                .Include(game => game.SellingInformation)
                                .Include(game => game.TradingInformation)
                                .ToListAsync();

            return gameEntities.ToContracts();
        }

        public async Task<Game> GetByIdAsync(Guid id)
        {
            var gameEntity = await _context.Games
                                .Include(game => game.SellingInformation)
                                .Include(game => game.TradingInformation)
                                .SingleOrDefaultAsync(game => game.Id == id);

            return gameEntity.ToContract();
        }

        public async Task<IEnumerable<Game>> GetByNameAsync(string name)
        {
            var gameEntities = await _context.Games
                                .Where(game => game.Name == name)
                                .Include(game => game.SellingInformation)
                                .Include(game => game.TradingInformation)
                                .ToListAsync();

            return gameEntities.ToContracts();
        }

        public Task<int> UpdateAsync(Guid productId, Game product)
        {
            var gameEntity = product.ToEntity();
            gameEntity.Id = productId;

            _context.Entry<EfGame>(gameEntity).State = EntityState.Modified;

            return _context.SaveChangesAsync();
        }
    }
}