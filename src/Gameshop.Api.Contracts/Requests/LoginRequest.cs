﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gameshop.Api.Contracts.Requests
{
    public class LoginRequest : ApiRequest
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
