using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;

namespace GameShop.Data.Providers
{
    public class MySqlDatabaseConnectionProvider : DatabaseConnectionProvider
    {
        public MySqlDatabaseConnectionProvider(string connectionString)
            : base(connectionString)
        {

        }

        public override DbConnection Get()
        {
            //Currently not supported.
            //return new MySqlConnection(ConnectionString);
            return null;
        }
    }
}
