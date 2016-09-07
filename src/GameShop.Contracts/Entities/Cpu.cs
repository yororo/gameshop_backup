using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameShop.Contracts.Entities
{
    public class CPU
    {
        public string Name { get; set; }
        public string ClockSpeed { get; set; }
        public int Cores { get; set; }
    }
}
