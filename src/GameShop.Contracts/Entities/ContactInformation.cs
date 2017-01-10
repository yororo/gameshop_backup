using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameShop.Contracts.Entities
{
    public class ContactInformation
    {
        #region Declarations
        
        private string _email;
        private string _phoneNumber;

        #endregion Declarations

        #region Properties

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

        public string PhoneNumber
        {
            get
            {
                return _phoneNumber;
            }

            set
            {
                _phoneNumber = value;
            }
        }

        #endregion Properties

        #region Constructors

        public ContactInformation()
        {
            _email = string.Empty;
            _phoneNumber = string.Empty;
        }

        #endregion Constructors
    }
}
