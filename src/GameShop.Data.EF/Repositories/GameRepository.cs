using GameShop.Data.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GameShop.Contracts.Entities;
using GameShop.Contracts.Enumerations;
using GameShop.Data.EF.Contexts;
using EFEntities = GameShop.Data.EF.Entities;
using GameShop.Data.EF.Translators;

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
            
            return 1;
        }

        public async Task<IEnumerable<Game>> GetAllAsync()
        {
            var games = new List<Game>();

            foreach(EFEntities.Games.Game game in _context.Games.ToList())
            {
                games.Add(game.ToGameContract());
            }

            return games;
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
    }
}
