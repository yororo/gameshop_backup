using GameShop.Contracts.Enumerations;
using System;

namespace GameShop.Contracts.Entities
{
    public class PricingInformation
    {
        public Guid PricingInformationId { get; set; }
        public decimal Price { get; set; }
        public Currency Currency { get; set; }
        public DateTime Created { get; set; }
        public DateTime Modified { get; set; }
    }
}