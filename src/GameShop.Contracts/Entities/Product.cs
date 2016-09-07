using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameShop.Contracts.Entities
{
    public abstract class Product
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime Created { get; set; }
        public DateTime Modified { get; set; }

        public Product()
        {
            Id = Guid.Empty;
            Name = string.Empty;
            Description = string.Empty;
            Created = DateTime.MaxValue;
            Modified = DateTime.MaxValue;
        }
    }
}
