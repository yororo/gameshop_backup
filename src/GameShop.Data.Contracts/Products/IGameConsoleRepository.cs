using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GameShop.Contracts.Entities.Products;

namespace GameShop.Data.Contracts.Products
{
    public interface IGameConsoleRepository<TId> : IProductRepository<TId, GameConsole>
    {
         Task<IEnumerable<GameConsole>> GetByPlatformAsync(string platform);
    }

    public interface IGameConsoleRepository : IGameConsoleRepository<Guid>
    {
        
    }
}