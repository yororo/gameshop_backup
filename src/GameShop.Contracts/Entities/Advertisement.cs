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
        private AdStatus _adStatusId;
        private string _title;
        private string _description;
        private string _reasonForSelling;
        private List<TProduct> _products;
        private List<Address> _meetupLocations;
        private AuditInformation _auditInformation;

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

        public AdStatus AdStatusId
        {
            get
            {
                return _adStatusId;
            }

            set
            {
                _adStatusId = value;
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

        public List<Address> MeetupLocations
        {
            get
            {
                return _meetupLocations;
            }

            set
            {
                _meetupLocations = value;
            }
        }

        public AuditInformation AuditInformation
        {
            get
            {
                return _auditInformation;
            }

            set
            {
                _auditInformation = value;
            }
        }

        #endregion

        #region Constructors

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