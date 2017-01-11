using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameShop.Authorization.Models
{
    public class ProfileAddress : Address
    {
        public Guid ProfileId { get; set; }
        public virtual Profile Profile { get; set; }
    }
}
