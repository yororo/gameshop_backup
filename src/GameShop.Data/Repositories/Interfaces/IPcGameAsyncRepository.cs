using GameShop.Contracts.Entities;
using GameShop.Contracts.Enumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameShop.Data.Repositories.Interfaces
{
    public interface IPCGameAsyncRepository : IProductAsyncRepository<PCGame, Guid>
    {
    }

    public interface IPCGameAsyncRepository<TId> : IProductAsyncRepository<PCGame, TId>
    {
    }
}
