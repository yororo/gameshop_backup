using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameShop.Contracts.Entities
{
    public class ContactInformation
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public string ContactNumber { get; set; }
        public DateTime Created { get; set; }
        public DateTime Modified { get; set; }
    }
}
