using GameShop.Interface.Enumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameShop.Interface.Entities
{
    public class PCGame : Game
    {
        public PCGame()
        {
            MinimumRequirements = string.Empty;
        }

        public string MinimumRequirements { get; set; }
    }
}
