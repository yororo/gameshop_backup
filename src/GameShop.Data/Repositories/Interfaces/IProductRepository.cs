using GameShop.Contracts.Entities;
using GameShop.Contracts.Enumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameShop.Data.Repositories.Interfaces
{
    /// <summary>
    /// Product Repository which uses <typeparamref name="Guid"/> as default type for ID.
    /// </summary>
    /// <typeparam name="T">Object type.</typeparam>
    public interface IProductRepository<T> where T : Product
    {
        IEnumerable<T> GetAll();
        IEnumerable<T> GetByName(string name);
        IEnumerable<T> GetByGenre(GameGenre genre);
        T GetById(Guid id);
    }

    /// <summary>
    /// Product Repository.
    /// </summary>
    /// <typeparam name="T">Object type.</typeparam>
    /// <typeparam name="TId">Type to use for an ID.</typeparam>
    public interface IProductRepository<T, TId> where T : Product
    {
        IEnumerable<T> GetAll();
        IEnumerable<T> GetByName(string name);
        IEnumerable<T> GetByGenre(GameGenre genre);
        T GetById(TId id);
    }
}
