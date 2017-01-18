using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameShop.Contracts.Entities
{
    public class ContactInformation
    {
        #region Properties

        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        #endregion Properties

        #region Constructors

        /// <summary>
        /// Default constructor.
        /// </summary>
        public ContactInformation()
        {
            Email = string.Empty;
            PhoneNumber = string.Empty;
        }

        #endregion Constructors
    }
}
