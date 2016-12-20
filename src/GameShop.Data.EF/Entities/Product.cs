using GameShop.Contracts.Enumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameShop.Data.EF.Entities
{
    internal class Product : Entity
    {
        public virtual Advertisement Advertisement { get; set; }
        public Guid ProductId { get;set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public virtual SellingInformation SellingInformation { get; set; }
        public virtual TradingInformation TradingInformation { get; set; }
        public ProductState State { get; set; }
    }
}
