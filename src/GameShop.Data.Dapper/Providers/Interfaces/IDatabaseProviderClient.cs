using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;

namespace GameShop.Data.Providers.Interfaces
{
    public interface IDatabaseProviderClient
    {
        /// <summary>
        /// Database connection string.
        /// </summary>
        string ConnectionString { get; set; }

        /// <summary>
        /// Create a database connection.
        /// </summary>
        /// <returns>Database provider's connection.</returns>
        DbConnection CreateConnection();
    }
}
