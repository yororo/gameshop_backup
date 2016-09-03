using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;

namespace GameShop.Data.Providers.Interfaces
{
    public interface IDatabaseConnectionProvider : IProvider<DbConnection>
    {
        /// <summary>
        /// Database connection string.
        /// </summary>
        string ConnectionString { get; set; }
    }
}
