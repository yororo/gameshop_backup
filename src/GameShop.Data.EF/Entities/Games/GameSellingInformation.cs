using System;

namespace GameShop.Data.EF.Entities.Games
{
    internal class GameSellingInformation : SellingInformation
    {
        public Guid GameId { get; set; }
        public virtual Game Game { get; set; }
    }
}