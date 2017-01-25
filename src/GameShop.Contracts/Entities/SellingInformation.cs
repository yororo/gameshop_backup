using GameShop.Contracts.Enumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameShop.Contracts.Entities
{
    public class SellingInformation
    {
        #region Properties

        //public Guid Id { get; set; }

        public Currency Currency { get; set; }

        public decimal SellingPrice { get; set; }

        public string ReasonForSelling { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime ModifiedDate { get; set; }

        #endregion Properties

        #region Constructors
            
        /// <summary>
        /// Dafault constructor.
        /// </summary>
        public SellingInformation()
        {
            //Id = Guid.Empty;
            Currency = Currency.PHP;
            SellingPrice = decimal.Zero;
            ReasonForSelling = string.Empty;
            CreatedDate = DateTime.MaxValue;
            ModifiedDate = DateTime.MaxValue;
        }

        #endregion Constructors
    }
}
