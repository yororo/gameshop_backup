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
        public IEnumerable<AddressInformation> Addresses { get; set; }
        public IEnumerable<ContactInformation> ContactDetails { get; set; }
        public DateTime Created { get; set; }
        public DateTime Modified { get; set; }
    }
}
