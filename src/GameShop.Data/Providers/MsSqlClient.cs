using GameShop.Data.Providers.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace GameShop.Data.Providers
{
    public class MsSqlClient : DatabaseClient
    {
        public MsSqlClient(string connectionString)
            : base(connectionString)
        {

        }

        public override DbConnection CreateConnection()
        {
            var connection = SqlClientFactory.Instance.CreateConnection();
            //Populate connection string.
            connection.ConnectionString = ConnectionString;

            return connection;
        }

        public override DbCommand CreateCommand()
        {
            var command = SqlClientFactory.Instance.CreateCommand();
            command.Connection = CreateConnection();

            return command;
        }

        public override DbConnectionStringBuilder CreateConnectionStringBuilder()
        {
            var connectionStringBuilder = SqlClientFactory.Instance.CreateConnectionStringBuilder();
            connectionStringBuilder.ConnectionString = ConnectionString;

            return connectionStringBuilder;
        }

        public override DbParameter CreateParameter()
        {
            return SqlClientFactory.Instance.CreateParameter();
        }
    }
}
