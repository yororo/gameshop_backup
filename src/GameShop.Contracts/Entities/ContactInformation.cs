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

        public string ContactNumber { get; set; }

        #endregion Properties

        #region Constructors

        /// <summary>
        /// Default constructor.
        /// </summary>
        public ContactInformation()
        {
            Email = string.Empty;
            ContactNumber = string.Empty;
        }

        #endregion Constructors
    }
}
