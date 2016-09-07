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
        /// Database provider factory.
        /// </summary>
        public DbProviderFactory DatabaseProviderFactory { get; set; }

        /// <summary>
        /// Name of the repository's database table.
        /// </summary>
        protected string TableName { get; set; }

        public Repository(IDatabaseProviderFactory factory)
        {
            DatabaseProviderFactory = factory as DbProviderFactory;

            //Defaults
            TableName = this.GetType().Name;
        }
        public Repository(DbProviderFactory factory)
        {
            DatabaseProviderFactory = factory;

            //Defaults
            TableName = this.GetType().Name;
        }

    }
}
