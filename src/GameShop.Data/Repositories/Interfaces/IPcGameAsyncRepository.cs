using GameShop.Contracts.Entities;
using GameShop.Contracts.Enumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameShop.Data.Repositories.Interfaces
{
    public interface IPcGameAsyncRepository : IProductAsyncRepository<PCGame, Guid>
    {
    }

    public interface IPcGameAsyncRepository<TId> : IProductAsyncRepository<PCGame, TId>
    {
    }
}
