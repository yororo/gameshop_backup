using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameShop.Contracts.Enumerations
{
    public enum ProductType : short
    {
        Unspecified = 0,
        ForSale = 1,
        ForTrade = 2,
        ForSaleOrTrade = ForSale | ForTrade,
        Sold,
        Traded
    }
}
