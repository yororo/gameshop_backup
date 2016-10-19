using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Dapper;
using GameShop.Data.Providers;
using GameShop.Data.Providers.Interfaces;

namespace GameShop.Data.Repositories
{
    public class Repository
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
