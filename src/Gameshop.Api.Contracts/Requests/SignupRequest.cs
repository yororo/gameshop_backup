using GameShop.Contracts.Entities;
using GameShop.Contracts.Enumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace GameShop.Api.Contracts.Requests
{
    public class SignupRequest : ApiRequest
    {
        [Required]
        public string Username { get; set; }
        public string Email { get; set; }
        public string MobileNumber { get; set; }
        public Salutation Salutation { get; set; }
        [Required]
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        [Required]
        public string LastName { get; set; }
        public string Suffix { get; set; }
        public Gender Gender { get; set; }
        [Required]
        public DateTime Birthday { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
