using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameShop.Contracts.Entities
{
    public class User
    {
        public Guid Id { get; set; }
        public Name Name { get; set; }
        public Address Address { get; set; }
        public ContactInformation ContactDetails { get; set; }
    }
}
