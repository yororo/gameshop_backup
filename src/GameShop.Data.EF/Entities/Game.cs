using GameShop.Contracts.Enumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameShop.Data.EF.Entities
{
    internal class Game : Product
    {
        string Title { get; set; }
        GameGenre GameGenre { get; set; }
        GamePlatform GamePlatform { get; set; }
        
    }
}
