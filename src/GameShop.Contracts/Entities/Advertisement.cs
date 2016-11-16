using GameShop.Contracts.Enumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameShop.Contracts.Entities
{
    public class Advertisement<TProduct> where TProduct : Product
    {
        #region Declarations

        private Guid _advertisementId;
        private string _friendlyId;
        private string _title;
        private string _description;
        private List<TProduct> _products;
        private MeetupInformation _meetupInformation;
        private AdvertisementState _state;
        private User _owner;
        private DateTime _createdDate;
        private DateTime _modifiedDate;

        #endregion Declarations

        #region Properties

        public Guid AdvertisementId
        {
            get
            {
                return _advertisementId;
            }

            set
            {
                _advertisementId = value;
            }
        }

        public string FriendlyId
        {
            get
            {
                return _friendlyId;
            }

            set
            {
                _friendlyId = value;
            }
        }

        public string Title
        {
            get
            {
                return _title;
            }

            set
            {
                _title = value;
            }
        }

        public string Description
        {
            get
            {
                return _description;
            }

            set
            {
                _description = value;
            }
        }

        public AdvertisementState State
        {
            get
            {
                return _state;
            }

            set
            {
                _state = value;
            }
        }

        public List<TProduct> Products
        {
            get
            {
                return _products;
            }

            set
            {
                _products = value;
            }
        }

        public MeetupInformation MeetupInformation
        {
            get
            {
                return _meetupInformation;
            }

            set
            {
                _meetupInformation = value;
            }
        }
        
        public User Owner
        {
            get
            {
                return _owner;
            }
            set
            {
                _owner = value;
            }
        }

        public DateTime CreatedDate
        {
            get
            {
                return _createdDate;
            }
            set
            {
                _createdDate = value;
            }
        }

        public DateTime ModifiedDate
        {
            get
            {
                return _modifiedDate;
            }
            set
            {
                _modifiedDate = value;
            }
        }

        #endregion Properties

        #region Constructors

        public Advertisement()
        {
            _advertisementId = Guid.Empty;
            _friendlyId = string.Empty;
            _title = string.Empty;
            _description = string.Empty;
            _products = new List<TProduct>();
            _meetupInformation = new MeetupInformation();
            _state = AdvertisementState.Inactive;
            _createdDate = DateTime.MaxValue;
            _modifiedDate = DateTime.MaxValue;
        }

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

        public Advertisement()
            : base()
        {

        }

        public Advertisement(IEnumerable<Product> products)
            : base(products)
        {

        }

        #endregion Constructors
    }
}