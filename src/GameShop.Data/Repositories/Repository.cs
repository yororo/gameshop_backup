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
    public abstract class Repository
    {
        /// <summary>
        /// Database provider client.
        /// </summary>
        public IDatabaseProviderClient Client { get; set; }
        
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="databaseProviderClient"></param>
        public Repository(IDatabaseProviderClient databaseProviderClient)
        {
            Client = databaseProviderClient;
        }
    }
}
