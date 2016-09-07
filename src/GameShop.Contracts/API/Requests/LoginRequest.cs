using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameShop.Contracts.API.Requests
{
    public class LoginRequest : APIRequest
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
