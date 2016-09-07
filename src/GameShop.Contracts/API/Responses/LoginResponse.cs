using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameShop.Contracts.API.Responses
{
    public class LoginResponse : APIResponse
    {
        public string Token { get; set; }
    }
}
