﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameShop.Contracts.Enumerations
{
    public enum ProductState : short
    {
        ForSale = 1,
        ForTrade,
        ForSaleOrTrade,
        Sold,
        Traded
    }
}