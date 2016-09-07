using GameShop.Contracts.Enumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameShop.Contracts.Entities
{
    public class OperatingSystem
    {
        public OS Type { get; set; }
        public string Name { get; set; }
    }
}
