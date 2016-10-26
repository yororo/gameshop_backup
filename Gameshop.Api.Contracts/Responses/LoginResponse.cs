using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gameshop.Api.Contracts.Responses
{
    public class LoginResponse : ApiResponse
    {
        public string Token { get; set; }
    }
}
