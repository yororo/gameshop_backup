using GameShop.Data.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GameShop.Contracts.Entities;
using GameShop.Contracts.Enumerations;
using GameShop.Data.EF.Contexts;
using GameShop.Data.EF.Translators;
using Microsoft.EntityFrameworkCore;

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
            await _context.Games.AddAsync(game.ToGameEntity());
            return await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Game>> GetAllAsync()
        {
            var gameEntities = await _context.Games.ToListAsync();

            return gameEntities.ToGameContracts();
        }

        public async Task<IEnumerable<Game>> GetByGenreAsync(GameGenre genre)
        {
            var gameEntities = await _context.Games.Where(
                game => game.GameGenre == genre).ToListAsync();

            return gameEntities.ToGameContracts();
        }

        public async Task<Game> GetByIdAsync(Guid id)
        {
            var gameEntity = await _context.Games.SingleOrDefaultAsync(
                game => game.Id == id);

            return gameEntity.ToGameContract();
        }

        public async Task<IEnumerable<Game>> GetByNameAsync(string name)
        {
            // we compare lower case strings to avoid mismatch due to casing
            name = name.Trim().ToLower();
            
            var gameEntities = await _context.Games.Where(
                game => game.Name.Trim().ToLower() == name).ToListAsync();

            return gameEntities.ToGameContracts();
        }
    }
}