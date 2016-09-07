using GameShop.Data.Providers.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace GameShop.Data.Providers
{
    public class MsSqlClientCustomFactory : DatabaseProviderFactory
    {
        public MsSqlClientCustomFactory(string connectionString)
            : base(connectionString)
        {

        }

        public override DbConnection CreateConnection()
        {
            return SqlClientFactory.Instance.CreateConnection();
        }

        public override DbCommand CreateCommand()
        {
            return SqlClientFactory.Instance.CreateCommand();
        }

        public override DbConnectionStringBuilder CreateConnectionStringBuilder()
        {
            return SqlClientFactory.Instance.CreateConnectionStringBuilder();
        }

        public override DbParameter CreateParameter()
        {
            return SqlClientFactory.Instance.CreateParameter();
        }
    }
}
