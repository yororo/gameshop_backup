using GameShop.Contracts.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameShop.Data.Repositories.Interfaces
{
    /// <summary>
    /// Ad Repository which uses <typeparamref name="Guid"/> as default type for IDs.
    /// </summary>
    public interface IAdvertisementRepository : IAdvertisementRepository<Guid>
    {
    }

    /// <summary>
    /// Ad Repository.
    /// </summary>
    /// <typeparam name="TId">Type to use for an ID.</typeparam>
    public interface IAdvertisementRepository<TId>
    {
        Advertisement FindById(TId id);
        Advertisement FindByFriendlyId(string id);
        IEnumerable<Advertisement> FindByTitle(string title);
        IEnumerable<Advertisement> GetAll();
        int Add(Advertisement advertisement);
        int Update(TId id, Advertisement advertisement);
        int DeleteById(TId id);
    }
}
