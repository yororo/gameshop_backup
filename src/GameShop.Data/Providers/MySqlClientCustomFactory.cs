using Pomelo.Data.MySql;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;

namespace GameShop.Data.Providers
{
    public class MySqlClientCustomFactory : DatabaseProviderFactory
    {
        public MySqlClientCustomFactory(string connectionString)
            : base(connectionString)
        {

        }

        public override DbConnection CreateConnection()
        {
            return MySqlClientFactory.Instance.CreateConnection();
        }

        public override DbCommand CreateCommand()
        {
            return MySqlClientFactory.Instance.CreateCommand();
        }

        public override DbConnectionStringBuilder CreateConnectionStringBuilder()
        {
            var builder = MySqlClientFactory.Instance.CreateConnectionStringBuilder();
            builder.ConnectionString = ConnectionString;

            return builder;
        }

        public override DbParameter CreateParameter()
        {
            return MySqlClientFactory.Instance.CreateParameter();
        }
    }
}
