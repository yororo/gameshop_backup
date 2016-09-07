using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameShop.Contracts.Entities
{
    public class Ad<T> where T : Product
    {
        public Ad()
        {
            Products = new List<T>();
        }

        public Guid Id { get; set; }
        public string FriendlyId { get; set; }
        public string Title { get; set; }
        public List<T> Products { get; set; }
        public string Description { get; set; }
        public Address Location { get; set; }
        public User Owner { get; set; }
        public DateTime Created { get; set; }
        public DateTime Modified { get; set; }
    }

    public class Ad : Ad<Product>
    {
        
    }
}
