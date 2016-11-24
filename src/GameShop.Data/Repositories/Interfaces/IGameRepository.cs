using GameShop.Contracts.Entities;
using GameShop.Contracts.Enumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameShop.Data.Repositories.Interfaces
{
    public interface IGameRepository<TId> : IProductRepository<TId, Game>
    {
        Task<IEnumerable<Game>> GetByGenreAsync(GameGenre genre);
        Task<int> AddGameAsync(Game game);
    }
    public interface IGameRepository : IGameRepository<Guid>
    { }
}
