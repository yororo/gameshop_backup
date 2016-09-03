using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameShop.Interface.Entities
{
    public abstract class Product
    {
        public Product()
        {
            Id = Guid.Empty;
            Name = string.Empty;
            Description = string.Empty;
            CreatedAt = DateTime.MaxValue;
            ModifiedAt = DateTime.MaxValue;
        }

        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ModifiedAt { get; set; }
    }
}
