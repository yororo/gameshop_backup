using GameShop.Contracts.Enumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameShop.Contracts.Entities
{
    public class PcGame : Game
    {
        public PcGame()
        {
            SystemRequirements = new ComputerSpecification();
        }

        public ComputerSpecification SystemRequirements { get; set; }
    }
}
