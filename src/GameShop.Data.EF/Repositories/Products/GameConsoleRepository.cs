using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GameShop.Contracts.Entities;
using GameShop.Contracts.Entities.Products;
using GameShop.Data.Contracts.Products;
using GameShop.Data.EF.Contexts;
using GameShop.Data.EF.Entities.Products;
using Microsoft.EntityFrameworkCore;

namespace GameShop.Data.EF.Repositories.Products
{
    internal class GameConsoleRepository : IGameConsoleRepository
    {
        private readonly GameShopContext _context;

        public GameConsoleRepository(GameShopContext context)
        {
            _context = context;
        }
        public Task<int> AddAsync(GameConsole product)
        {
            _context.GameConsoles.Add(product.ToEntity());

            return _context.SaveChangesAsync();
        }

        public Task<int> DeleteByIdAsync(Guid productId)
        {
            _context.Entry<EfGameConsole>(new EfGameConsole(){ Id = productId }).State = EntityState.Deleted;
            
            return _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<GameConsole>> GetAllAsync()
        {
            var gameConsoleEntities = await _context.GameConsoles
                                                    .Include(gameConsole => gameConsole.SellingInformation)
                                                    .Include(gameConsole => gameConsole.TradingInformation)
                                                    .ToListAsync();

            return gameConsoleEntities.ToContracts();
        }

        public async Task<GameConsole> GetByIdAsync(Guid productId)
        {
            var gameConsoleEntity = await _context.GameConsoles
                                                    .Include(gameConsole => gameConsole.SellingInformation)
                                                    .Include(gameConsole => gameConsole.TradingInformation)
                                                    .FirstOrDefaultAsync(gameConsole => gameConsole.Id == productId);

            return gameConsoleEntity.ToContract();
        }

        public async Task<IEnumerable<GameConsole>> GetByNameAsync(string productName)
        {
            var gameConsoleEntities = await _context.GameConsoles
                                                    .Include(gameConsole => gameConsole.SellingInformation)
                                                    .Include(gameConsole => gameConsole.TradingInformation)
                                                    .Where(gameConsole => gameConsole.Name == productName)
                                                    .ToListAsync();

            return gameConsoleEntities.ToContracts();
        }

        public async Task<IEnumerable<GameConsole>> GetByPlatformAsync(string platform)
        {
            var gameConsoleEntities = await _context.GameConsoles
                                                    .Include(gameConsole => gameConsole.SellingInformation)
                                                    .Include(gameConsole => gameConsole.TradingInformation)
                                                    .Where(gameConsole => gameConsole.ConsolePlatform == platform)
                                                    .ToListAsync();

            return gameConsoleEntities.ToContracts();
        }

        public Task<int> UpdateAsync(Guid productId, GameConsole product)
        {
            var gameConsoleEntity = product.ToEntity();
            gameConsoleEntity.Id = product.Id;

            _context.Entry<EfGameConsole>(gameConsoleEntity).State = EntityState.Modified;

            return _context.SaveChangesAsync();
        }
    }
}