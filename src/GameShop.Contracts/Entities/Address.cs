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
        private string _street1;
        private string _street2;
        private string _street3;
        private string _barangay;
        private string _municipality;
        private string _city;
        private string _zipCode;
        private string _province;
        private string _region;
        private string _country;
        private DateTime _created;
        private DateTime _modified;

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

        public string Street1
        {
            get
            {
                return _street1;
            }

            set
            {
                _street1 = value;
            }
        }
        public string Street2
        {
            get
            {
                return _street2;
            }

            set
            {
                _street2 = value;
            }
        }
        public string Street3
        {
            get
            {
                return _street3;
            }

            set
            {
                _street3 = value;
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

        public DateTime Created
        {
            get
            {
                return _created;
            }

            set
            {
                _created = value;
            }
        }

        public DateTime Modified
        {
            get
            {
                return _modified;
            }

            set
            {
                _modified = value;
            }
        }

        #endregion

        #region Constructors

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
            Created = DateTime.MaxValue;
            Modified = DateTime.MaxValue;
        }

        #endregion
    }
}
