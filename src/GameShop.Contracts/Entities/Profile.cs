using GameShop.Contracts.Enumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameShop.Contracts.Entities
{
    public class Profile
    {
        #region Declarations

        private Guid _profileId;
        private Name _name;
        private Gender _gender;
        private CivilStatus _civilStatus;
        private DateTime _birthday;
        private List<Address> _addresses;
        private List<ContactInformation> _contactInformation;
        private DateTime _createdDate;
        private DateTime _modifiedDate;

        #endregion Declarations

        #region Properties

        public Guid ProfileId
        {
            get
            {
                return _profileId;
            }

            set
            {
                _profileId = value;
            }
        }

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

        public Gender Gender
        {
            get
            {
                return _gender;
            }

            set
            {
                _gender = value;
            }
        }

        public CivilStatus CivilStatus
        {
            get
            {
                return _civilStatus;
            }

            set
            {
                _civilStatus = value;
            }
        }

        public DateTime Birthday
        {
            get
            {
                return _birthday;
            }

            set
            {
                _birthday = value;
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

        /// <summary>
        /// Default constructor initialization.
        /// </summary>
        public Profile()
        {
            _profileId = Guid.Empty;
            _name = new Name();
            _gender = Gender.Unspecified;
            _civilStatus = CivilStatus.Unspecified;
            _birthday = DateTime.MaxValue;
            _addresses = new List<Address>();
            _contactInformation = new List<ContactInformation>();
            _createdDate = DateTime.MaxValue;
            _modifiedDate = DateTime.MaxValue;
        }

        #endregion Constructors
    }
}
