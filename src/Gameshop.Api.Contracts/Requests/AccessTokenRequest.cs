using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GameShop.Api.Contracts.Requests
{
    public class AccessTokenRequest : ApiRequest
    {
        [Required]
        public string AuthorizationCode { get; set; }
        public string State { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
