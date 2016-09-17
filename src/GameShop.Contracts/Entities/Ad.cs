using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GameShop.Contracts.Entities;

namespace GameShop.Contracts.Entities
{
    public class Ad : Ad<Product>
    {
        #region Fields
        
        #endregion

        #region Properties
        
        #endregion

        #region Constructors
        /// <summary>
        /// Default constructor initialization.
        /// </summary>
        public Ad()
        {

        }

        #region Private Methods

        #endregion

        #region Public Methods

        #endregion
    }

    public class Ad<T> where T : Product
    {
        #region Fields
        private Guid _adId;
        private string _friendlyId;
        private string _adTitle;
        private string _adDescription;
        private List<T> _products;
        private Address _location;
        private User _adOwner;
        private DateTime _adCreatedDTTM;
        private DateTime _adModifiedDTTM;
        #endregion

        #region Properties
        public Guid AdId
        {
            get
            {
                return _adId;
            }

            set
            {
                _adId = value;
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

        public string AdTitle
        {
            get
            {
                return _adTitle;
            }

            set
            {
                _adTitle = value;
            }
        }

        public string AdDescription
        {
            get
            {
                return _adDescription;
            }

            set
            {
                _adDescription = value;
            }
        }

        public List<T> Products
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

        public Address Location
        {
            get
            {
                return _location;
            }

            set
            {
                _location = value;
            }
        }

        public User AdOwner
        {
            get
            {
                return _adOwner;
            }

            set
            {
                _adOwner = value;
            }
        }

        public DateTime CreatedDTTM
        {
            get
            {
                return _adCreatedDTTM;
            }

            set
            {
                _adCreatedDTTM = value;
            }
        }

        public DateTime ModifiedDTTM
        {
            get
            {
                return _adModifiedDTTM;
            }

            set
            {
                _adModifiedDTTM = value;
            }
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Default constructor initialization.
        /// </summary>
        public Ad()
        {
            Products = new List<T>();
        }

        #region Private Methods

        #endregion

        #region Public Methods

        #endregion
    }
}
