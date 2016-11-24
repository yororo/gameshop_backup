using Dapper;
using GameShop.Contracts.Entities;
using GameShop.Data.Providers.Interfaces;
using GameShop.Data.Repositories.Interfaces;
using GameShop.Data.Translators;
using System;
using System.Collections.Generic;
using System.Data;
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
            string spGetAllProducts = @"spGetAllProducts";

            using (var databaseConnection = Client.CreateConnection())
            {
                var products = new List<Product>();

                var productsDatabase = 
                    await databaseConnection.
                    QueryAsync(spGetAllProducts, commandType: CommandType.StoredProcedure).
                    ConfigureAwait(false);

                foreach (var product in productsDatabase)
                {

                }

                return products;
            }
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
