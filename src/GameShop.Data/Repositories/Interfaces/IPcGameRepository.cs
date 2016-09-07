using GameShop.Contracts.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameShop.Data.Repositories.Interfaces
{
    public interface IPcGameRepository : IProductRepository<PcGame, Guid>
    {
    }

    public interface IPcGameRepository<TId> : IProductRepository<PcGame, TId>
    {

    }
}
