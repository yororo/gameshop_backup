using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameShop.Contracts.Entities
{
    public class ContactInformation
    {
        #region Declarations

        private Guid _contactInformationId;
        private string _email;
        private string _mobileNumber;
        private DateTime _createdDate;
        private DateTime _modifiedDate;

        #endregion Declarations

        #region Properties

        public Guid ContactInformationId
        {
            get
            {
                return _contactInformationId;
            }

            set
            {
                _contactInformationId = value;
            }
        }

        public string Email
        {
            get
            {
                return _email;
            }

            set
            {
                _email = value;
            }
        }

        public string MobileNumber
        {
            get
            {
                return _mobileNumber;
            }

            set
            {
                _mobileNumber = value;
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

        public ContactInformation()
        {
            _contactInformationId = Guid.Empty;
            _email = string.Empty;
            _mobileNumber = string.Empty;
            _createdDate = DateTime.MaxValue;
            _modifiedDate = DateTime.MaxValue;
        }

        #endregion Constructors
    }
}
