using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameShop.Contracts.Entities
{
    /// <summary>
    /// Address
    /// </summary>
    public class Address
    {
        #region Properties

        public string Street1 { get; set; }

        public string Street2 { get; set; }
        
        public string Street3 { get; set; }

        public string Barangay { get; set; }

        public string Municipality { get; set; }

        public string City { get; set; }

        public string ZipCode { get; set; }

        public string Province { get; set; }

        public string Region { get; set; }

        public string Country { get; set; }

        #endregion Properties

        #region Constructors

        /// <summary>
        /// Default constructor.
        /// </summary>
        public Address()
        {
            Street1 = string.Empty;
            Street2 = string.Empty;
            Street3 = string.Empty;
            Barangay = string.Empty;
            Municipality = string.Empty;
            City = string.Empty;
            ZipCode = string.Empty;
            Province = string.Empty;
            Region = string.Empty;
            Country = string.Empty;
        }

        #endregion Constructors
    }
}
