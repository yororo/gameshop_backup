using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using GameShop.Data.Providers.Interfaces;
using System.Data;
using System.Text;
using System.Data.Common;

namespace GameShop.Data.Repositories
{
    public class Repository
    {
        /// <summary>
        /// Database provider client.
        /// </summary>
        public IDatabaseClient DatabaseClient { get; set; }

        /// <summary>
        /// Name of the repository's database table.
        /// </summary>
        protected string TableName { get; set; }

        public Repository()
        {
            TableName = this.GetType().Name;
        }

        public Repository(IDatabaseClient factory)
            : this()
        {
            DatabaseClient = factory;
        }

        public Repository(DbProviderFactory factory)
            : this()
        {
            DatabaseClient = (IDatabaseClient)factory;
        }
    }
}
