using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GameShop.Api.Contracts.Requests
{
    public class SigninRequest : ApiRequest
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }

        public SigninRequest()
            : base()
        {
            Username = string.Empty;
            Password = string.Empty;
        }
    }
}
