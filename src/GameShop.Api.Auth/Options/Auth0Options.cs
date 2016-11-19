using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameShop.Api.Auth.Options
{
    public class Auth0Options
    {
        public string Domain { get; set; }

        public string CallbackUri { get; set; }

        public string ClientId { get; set; }
    }
}
