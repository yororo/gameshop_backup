using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameShop.Contracts.Entities
{
    public abstract class Person
    {
        #region Fields
        private Name _name;
        private Address _address;
        private ContactInformation _contactInfo;
        #endregion

        #region Properties
        public Name Name
        {
            get
            {
                return _name;
            }

            set
            {
                _name = value;
            }
        }

        public Address Address
        {
            get
            {
                return _address;
            }

            set
            {
                _address = value;
            }
        }

        public ContactInformation ContactInfo
        {
            get
            {
                return _contactInfo;
            }

            set
            {
                _contactInfo = value;
            }
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Default constructor initialization.
        /// </summary>
        public Person()
        {
            Name = new Name();
            Address = new Address();
            ContactInfo = new ContactInformation();
        }
        #endregion

        #region Private Methods

        #endregion

        #region Public Methods

        #endregion
    }
}
