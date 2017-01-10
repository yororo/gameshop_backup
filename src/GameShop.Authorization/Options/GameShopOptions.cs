using System.Collections.Generic;

namespace GameShop.Authorization.Options
{
    public class GameShopOptions
    {
        /// <summary>
        /// Valid resource servers that this authorization server issues tickets for.
        /// </summary>
        /// <returns></returns>
        public List<string> Resources { get; set; }
    }
}