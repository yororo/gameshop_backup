using GameShop.Data.Providers.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;

namespace GameShop.Data.Providers
{
    public abstract class DatabaseProviderClient : IDatabaseProviderClient
    {
        /// <summary>
        /// Database connection string.
        /// </summary>
        public string ConnectionString { get; set; }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="connectionString">Database connection string.</param>
        public DatabaseProviderClient(string connectionString)
        {
            ConnectionString = connectionString;
        }
        
        /// <summary>
        /// Create a database connection.
        /// </summary>
        /// <returns>Database provider connection.</returns>
        public abstract DbConnection CreateConnection();
    }
}
