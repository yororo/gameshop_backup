using GameShop.Contracts.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameShop.Data.Repositories.Interfaces
{
    /// <summary>
    /// Ad Async Repository which uses <typeparamref name="Guid"/> as default type for ID.
    /// </summary>
    public interface IGameAdvertisementRepository : IAdvertisementRepository<Game>
    {
    }
}
