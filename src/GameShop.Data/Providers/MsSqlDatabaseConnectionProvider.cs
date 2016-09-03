using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace GameShop.Data.Providers
{
    public class MsSqlDatabaseConnectionProvider : DatabaseConnectionProvider
    {
        public MsSqlDatabaseConnectionProvider(string connectionString)
            : base(connectionString)
        {

        }

        public override DbConnection Get()
        {
            return new SqlConnection(ConnectionString);
        }
    }
}
