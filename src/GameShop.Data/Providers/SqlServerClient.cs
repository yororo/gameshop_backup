using GameShop.Data.Providers.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace GameShop.Data.Providers
{
    public class SqlServerClient : DatabaseProviderClient
    {
        /// <summary>
        /// Contructor.
        /// </summary>
        /// <param name="connectionString">SQL Server connection string.</param>
        public SqlServerClient(string connectionString)
            : base(connectionString)
        {

        }

        /// <summary>
        /// Create a SQL Server database connection.
        /// </summary>
        /// <returns>SQL Server database connection.</returns>
        public override DbConnection CreateConnection()
        {
            return new SqlConnection(ConnectionString);
        }
    }
}
