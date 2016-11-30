using GameShop.Data.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GameShop.Contracts.Entities;
using GameShop.Contracts.Enumerations;
using GameShop.Data.EF.Contexts;
using EFEntities = GameShop.Data.EF.Entities;

namespace GameShop.Data.EF.Repositories
{
    public class GameRepository : IGameRepository
    {
        private GameShopContext _context;

        public GameRepository(GameShopContext context)
        {
            _context = context;
        }

        public Task<int> AddGameAsync(Game game)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Game>> GetAllAsync()
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
    }
}
