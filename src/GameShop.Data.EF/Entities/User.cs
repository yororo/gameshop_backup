using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameShop.Data.EF.Entities
{
    internal class User : Entity
    {
        public Guid UserId { get; set; }
        public virtual Account Account { get; set; }
        public virtual Profile Profile { get; set; }
    }
}
