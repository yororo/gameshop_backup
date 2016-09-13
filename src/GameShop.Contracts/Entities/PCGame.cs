using GameShop.Contracts.Enumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameShop.Contracts.Entities
{
    public class PCGame : Game
    {
        public PCGame()
        {
            SystemRequirements = new ComputerSpecification();
        }

        public ComputerSpecification SystemRequirements { get; set; }
    }
}
