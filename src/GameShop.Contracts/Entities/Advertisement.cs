using GameShop.Contracts.Enumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameShop.Contracts.Entities
{
    public class Advertisement<TProduct> where TProduct : Product
    {
        #region Fields

        private Guid _advertisementId;
        private string _friendlyId;
        private string _title;
        private string _description;
        private string _reasonForSelling;
        private List<TProduct> _products;
        private MeetupInformation _meetupInformation;
        private AdvertisementState _status;
        private DateTime _createdDate;
        private DateTime _modifiedDate;

        #endregion

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

        public AdvertisementState Status
        {
            get
            {
                return _status;
            }

            set
            {
                _status = value;
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

        public string ReasonForSelling
        {
            get
            {
                return _reasonForSelling;
            }

            set
            {
                _reasonForSelling = value;
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

        #endregion

        #region Constructors

        public Advertisement()
        {
            Products = new List<TProduct>();
            MeetupInformation = new MeetupInformation();
            CreatedDate = DateTime.MaxValue;
            ModifiedDate = DateTime.MaxValue;
        }

        public Advertisement(IEnumerable<TProduct> products)
            : this()
        {
            Products = products.ToList();
        }

        #endregion

        #region Private Methods



        #endregion

        #region Public Methods



        #endregion
    }

    public class Advertisement : Advertisement<Product>
    {
        #region Fields



        #endregion

        #region Properties



        #endregion

        #region Constructors

        public Advertisement()
            : base()
        {

        }

        public Advertisement(IEnumerable<Product> products)
            : base(products)
        {

        }

        #endregion

        #region Private Methods



        #endregion

        #region Public Methods



        #endregion
    }
}