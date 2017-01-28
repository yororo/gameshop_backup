using System;
using GameShop.Contracts.Enumerations;

namespace GameShop.Contracts.Entities.Products
{
    public class GameConsole : Product
    {
        public string ConsolePlatform { get; set; }
        
        public override ProductCategory Category
        {
            get
            {
                return ProductCategory.GameConsoles;
            }
        }
    }
}