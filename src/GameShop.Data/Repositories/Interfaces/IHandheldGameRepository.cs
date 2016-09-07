using GameShop.Contracts.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameShop.Data.Repositories.Interfaces
{
    public interface IHandheldGameRepository : IProductRepository<HandheldGame, Guid>
    {
    }
    public interface IHandheldGameRepository<TId> : IProductRepository<HandheldGame, TId>
    {
    }
}
