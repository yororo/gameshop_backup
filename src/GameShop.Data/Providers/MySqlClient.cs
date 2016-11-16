using Pomelo.Data.MySql;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;

namespace GameShop.Data.Providers
{
    public class MySqlClient : DatabaseProviderClient
    {
        /// <summary>
        /// Contructor.
        /// </summary>
        /// <param name="connectionString">MySQL connection string.</param>
        public MySqlClient(string connectionString)
            : base(connectionString)
        {

        }

        /// <summary>
        /// Create a MySQL database connection.
        /// </summary>
        /// <returns>MySQL database connection.</returns>
        public override DbConnection CreateConnection()
        {
            return new MySqlConnection(ConnectionString);
        }
    }
}
