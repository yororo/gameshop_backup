using GameShop.Contracts.Entities;
using GameShop.Contracts.Enumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameShop.Data.Repositories.Interfaces
{
    /// <summary>
    /// This will implement the use of Guid and Product types.
    /// </summary>
    public interface IProductRepository : IProductRepository<Product>
    {

    }

    /// <summary>
    /// Product Async Repository which uses Guid as default type for ID.
    /// </summary>
    /// <typeparam name="TProduct">Product type.</typeparam>
    public interface IProductRepository<TProduct> : IProductRepository<Guid, TProduct> 
        where TProduct : Product
    {

    }

    /// <summary>
    /// Product Async Repository.
    /// </summary>
    /// <typeparam name="TProduct">Object type.</typeparam>
    /// <typeparam name="TId">Type to use for an ID.</typeparam>
    public interface IProductRepository<TId, TProduct> where TProduct : Product
    {
        Task<IEnumerable<TProduct>> GetAllAsync();
        Task<IEnumerable<TProduct>> GetByNameAsync(string name);
        Task<TProduct> GetByIdAsync(TId id);
    }
}
