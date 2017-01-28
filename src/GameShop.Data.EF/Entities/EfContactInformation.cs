using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameShop.Data.EF.Entities
{
    internal class EfContactInformation : EfEntity
    {
        public string Email { get; set; }
        public string ContactNumber { get; set; }
    }
}
