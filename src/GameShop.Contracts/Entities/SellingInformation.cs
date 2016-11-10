using GameShop.Contracts.Enumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameShop.Contracts.Entities
{
    public class SellingInformation
    {
        #region Declarations

        private Guid _sellingInformationId;
        private Currency _currency;
        private decimal _sellingPrice;
        private string _reasonForSelling;
        private DateTime _createdDate;
        private DateTime _modifiedDate;

        #endregion Declarations

        #region Properties

        public Guid SellingInformationId
        {
            get { return _sellingInformationId; }
            set { _sellingInformationId = value; }
        }

        public Currency Currency
        {
            get { return _currency; }
            set { _currency = value; }
        }

        public decimal SellingPrice
        {
            get { return _sellingPrice; }
            set { _sellingPrice = value; }
        }

        public string ReasonForSelling
        {
            get { return _reasonForSelling;}
            set { _reasonForSelling = value; }
        }

        public DateTime CreatedDate
        {
            get { return _createdDate; }
            set { _createdDate = value; }
        }

        public DateTime ModifiedDate
        {
            get { return _modifiedDate; }
            set { _modifiedDate = value; }
        }

        #endregion Properties
    }
}
