using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameShop.Data.Providers.Interfaces
{
    public interface IProvider<T>
    {
        /// <summary>
        /// Polymorphically produces a database connection.
        /// </summary>
        /// <returns>Database connection.</returns>
        T Get();
    }
}
