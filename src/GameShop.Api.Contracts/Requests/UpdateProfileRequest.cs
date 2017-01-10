using GameShop.Contracts.Entities;
using GameShop.Contracts.Enumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameShop.Api.Contracts.Requests
{
    public class UpdateProfileRequest : ApiRequest
    {
        public Guid UserId { get; set; }
        public Name Name { get; set; }
        public Gender Gender { get; set; }
        public CivilStatus CivilStatus { get; set; }
        public DateTime Birthday { get; set; }
        public List<Address> Addresses { get; set; }
        public List<ContactInformation> ContactInformation { get; set; }
    }
}
