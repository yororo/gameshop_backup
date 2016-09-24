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
        public MySqlClient(string connectionString)
            : base(connectionString)
        {

        }

        public override DbConnection CreateConnection()
        {
            return new MySqlConnection(ConnectionString);
        }
    }
}
