using GameShop.Contracts.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameShop.Data.Repositories.Interfaces
{
    public interface IHandheldGameAsyncRepository : IProductAsyncRepository<HandheldGame, Guid>
    {
    }
    public interface IHandheldGameAsyncRepository<TId> : IProductAsyncRepository<HandheldGame, TId>
    {
    }
}
