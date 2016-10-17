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
        private List<Address> _addresses;
        private List<ContactInformation> _contactInformation;
        private DateTime _createdDTTM;
        private DateTime _modifiedDTTM;
        private User _createdBy;
        private User _modifiedBy;

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

        public List<Address> Addresses
        {
            get
            {
                return _addresses;
            }

            set
            {
                _addresses = value;
            }
        }

        public List<ContactInformation> ContactInformation
        {
            get
            {
                return _contactInformation;
            }

            set
            {
                _contactInformation = value;
            }
        }

        public DateTime CreatedDTTM
        {
            get { return _createdDTTM; }
            set { _createdDTTM = value; }
        }

        public DateTime ModifiedDTTM
        {
            get { return _modifiedDTTM; }
            set { _modifiedDTTM = value; }
        }

        public User CreatedBy
        {
            get { return _createdBy; }
            set { _createdBy = value; }
        }

        public User ModifiedBy
        {
            get { return _modifiedBy; }
            set { _modifiedBy = value; }
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor initialization.
        /// </summary>
        public Person()
        {
            Name = new Name();
            Addresses = new List<Address>();
            ContactInformation = new List<ContactInformation>();
            CreatedDTTM = DateTime.MaxValue;
            ModifiedDTTM = DateTime.MaxValue;
            CreatedBy = new User();
            ModifiedBy = new User();
        }

        #endregion
    }
}
