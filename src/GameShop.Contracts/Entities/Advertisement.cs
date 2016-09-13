using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameShop.Contracts.Entities
{
    public class Advertisement<TProduct> where TProduct : Product
    {
        public Advertisement()
        {
            Products = new List<TProduct>();
        }

        public Advertisement(IEnumerable<TProduct> products)
        {
            Products = products;
        }

        public Guid Id { get; set; }
        public string FriendlyId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public IEnumerable<TProduct> Products { get; set; }
        public IEnumerable<Address> Locations { get; set; }
        public User Owner { get; set; }
        public DateTime Created { get; set; }
        public DateTime Modified { get; set; }
    }

    public class Advertisement : Advertisement<Product>
    {
        public Advertisement()
            : base()
        {
        }

        public Advertisement(IEnumerable<Product> products)
            : base(products)
        {
        }
    }
}
