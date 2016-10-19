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
        /// Constructor.
        /// </summary>
        /// <param name="connectionString">Connection string.</param>
        public SqlServerClient(string connectionString)
            : base(connectionString)
        {

        }

        /// <summary>
        /// Create a Sql Server connection.
        /// </summary>
        /// <returns>Sql Server database connection.</returns>
        public override DbConnection CreateConnection()
        {
            return new SqlConnection(ConnectionString);
        }
    }
}
