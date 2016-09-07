using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;

namespace GameShop.Data.Providers.Interfaces
{
    public interface IDatabaseProviderFactory
    {
        /// <summary>
        /// Database connection string.
        /// </summary>
        string ConnectionString { get; set; }
        
        DbCommand CreateCommand();
        DbConnection CreateConnection();
        DbConnectionStringBuilder CreateConnectionStringBuilder();
        DbParameter CreateParameter();
    }
}
