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
        public SqlServerClient(string connectionString)
            : base(connectionString)
        {

        }

        public override DbConnection CreateConnection()
        {
            return new SqlConnection(ConnectionString);
        }
    }
}
