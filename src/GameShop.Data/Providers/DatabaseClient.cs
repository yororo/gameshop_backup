using GameShop.Data.Providers.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;

namespace GameShop.Data.Providers
{
    public abstract class DatabaseClient : DbProviderFactory, IDatabaseClient
    {
        /// <summary>
        /// Database connection string.
        /// </summary>
        public string ConnectionString { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="connectionString">Database connection string.</param>
        public DatabaseClient(string connectionString)
        {
            ConnectionString = connectionString;
        }
        
        public abstract override DbCommand CreateCommand();
        public abstract override DbConnection CreateConnection();
        public abstract override DbConnectionStringBuilder CreateConnectionStringBuilder();
        public abstract override DbParameter CreateParameter();
    }
}
