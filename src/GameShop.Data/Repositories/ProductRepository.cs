using GameShop.Contracts.Entities;
using GameShop.Data.Providers.Interfaces;
using GameShop.Data.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameShop.Data.Repositories
{
    public class ProductRepository : Repository, IProductRepository
    {
        #region Constructors
        /// <summary>
        /// Default constructor.
        /// </summary>
        /// <param name="databaseProvider">The Database Provider (MSSQL/MYSQL/etc.)</param>
        public ProductRepository(IDatabaseProviderClient databaseProvider)
            : base (databaseProvider)
        { }
        #endregion Constructors

        #region Methods
        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<Product> GetByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Product>> GetByNameAsync(string name)
        {
            throw new NotImplementedException();
        }
        #endregion Methods
    }
}
