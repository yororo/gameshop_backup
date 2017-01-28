using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using GameShop.Contracts.Entities.Products;
using GameShop.Contracts.Enumerations;

namespace GameShop.Contracts.Entities
{
    public class Advertisement<TProduct> where TProduct : Product
    {
        #region Properties

        public Guid Id { get; set; }

        public string FriendlyId { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public AdvertisementState State { get; set; }

        public List<TProduct> Products { get; set; }

        public MeetupInformation MeetupInformation { get; set; }
        
        //public User Owner { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime ModifiedDate { get; set; }

        #endregion Properties

        #region Constructors

        /// <summary>
        /// Default constructor.
        /// </summary>
        public Advertisement()
        {
            Id = Guid.Empty;
            FriendlyId = string.Empty;
            Title = string.Empty;
            Description = string.Empty;
            Products = new List<TProduct>();
            State = AdvertisementState.Inactive;
            MeetupInformation = new MeetupInformation();
            //Owner = new User();
            CreatedDate = DateTime.MaxValue;
            ModifiedDate = DateTime.MaxValue;
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="products">Advertisement products.</param>
        public Advertisement(IEnumerable<TProduct> products)
            : this()
        {
            Products = products.ToList();
        }

        #endregion Constructors
    }

    public class Advertisement : Advertisement<Product>
    {
        #region Constructors

        /// <summary>
        /// Default contructor.
        /// </summary>
        public Advertisement()
            : base()
        {
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="products">Advertisement products.</param>
        public Advertisement(IEnumerable<Product> products)
            : base(products)
        {
        }

        #endregion Constructors
    }
}