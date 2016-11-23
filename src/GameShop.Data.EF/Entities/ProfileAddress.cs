using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameShop.Data.EF.Entities
{
    internal class ProfileAddress : Address
    {
        public Guid ProfileId { get; set; }
        public virtual Profile Profile { get; set; }
    }
}
