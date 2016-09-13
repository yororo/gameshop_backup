using GameShop.Contracts.Enumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameShop.Contracts.API.Responses
{
    public abstract class APIResponse
    {
        public Result Result { get; set; }
    }
}
