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
            MeetupLocations = new List<Address>();
        }

        public Advertisement(IEnumerable<TProduct> products)
            : this()
        {
            Products = products.ToList();
        }

        public Guid AdvertisementId { get; set; }
        public string FriendlyId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public List<TProduct> Products { get; set; }
        public List<Address> MeetupLocations { get; set; }
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
