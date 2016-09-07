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
    public interface IAdRepository
    {
        Ad FindById(Guid id);
        Ad FindByFriendlyId(string id);
        IEnumerable<Ad> FindByTitle(string title);
        IEnumerable<Ad> GetAll();
        int DeleteById(Guid id);
    }

    /// <summary>
    /// Ad Repository.
    /// </summary>
    /// <typeparam name="TId">Type to use for an ID.</typeparam>
    public interface IAdRepository<TId>
    {
        Ad FindById(TId id);
        Ad FindByFriendlyId(TId id);
        IEnumerable<Ad> FindByTitle(string title);
        IEnumerable<Ad> GetAll();
        int DeleteById(TId id);
    }
}
