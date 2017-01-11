using GameShop.Contracts.Enumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameShop.Data.EF.Entities
{
    internal abstract class Product : Entity
    {
        public Guid Id { get;set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public ProductState State { get; set; }
    }
}
