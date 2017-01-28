using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using GameShop.Contracts.Entities.Products;
using GameShop.Contracts.Enumerations;

namespace GameShop.Data.Contracts.Products
{
    public interface IGameRepository<TId> : IProductRepository<TId, Game>
    {
        Task<IEnumerable<Game>> GetByGenreAsync(GameGenre genre);
    }

    public interface IGameRepository : IGameRepository<Guid>
    {

    }
}
