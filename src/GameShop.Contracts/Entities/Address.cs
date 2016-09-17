using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameShop.Contracts.Entities
{
    public class Address
    {
        #region Fields
        private Guid _addressId;
        private string _barangay;
        private string _municipality;
        private string _city;
        private string _zipCode;
        private string _province;
        private string _region;
        private string _country;
        #endregion

        #region Properties
        public Guid AddressId
        {
            get
            {
                return _addressId;
            }

            set
            {
                _addressId = value;
            }
        }

        public string Barangay
        {
            get
            {
                return _barangay;
            }

            set
            {
                _barangay = value;
            }
        }

        public string Municipality
        {
            get
            {
                return _municipality;
            }

            set
            {
                _municipality = value;
            }
        }

        public string City
        {
            get
            {
                return _city;
            }

            set
            {
                _city = value;
            }
        }

        public string ZipCode
        {
            get
            {
                return _zipCode;
            }

            set
            {
                _zipCode = value;
            }
        }

        public string Province
        {
            get
            {
                return _province;
            }

            set
            {
                _province = value;
            }
        }

        public string Region
        {
            get
            {
                return _region;
            }

            set
            {
                _region = value;
            }
        }

        public string Country
        {
            get
            {
                return _country;
            }

            set
            {
                _country = value;
            }
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Default constructor initialization.
        /// </summary>
        public Address()
        {
            AddressId = Guid.Empty;
            Barangay = string.Empty;
            Municipality = string.Empty;
            City = string.Empty;
            ZipCode = string.Empty;
            Province = string.Empty;
            Region = string.Empty;
            Country = string.Empty;
        }
        #endregion

        #region Private Methods

        #endregion

        #region Public Methods

        #endregion


    }
}
