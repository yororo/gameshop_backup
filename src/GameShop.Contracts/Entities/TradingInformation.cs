using GameShop.Contracts.Enumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameShop.Contracts.Entities
{
    public class TradingInformation
    {
        #region Declarations

        private Guid _tradingInformationId;
        private Currency _currency;
        private decimal _tradingPrice;
        private string _reasonForTrading;
        private bool _isOwnerwillingToAddCash;
        private decimal _cashAmountToAdd;
        private bool _isOwnerWillingToReceiveCash;
        private string _tradeNotes;
        private DateTime _createdDate;
        private DateTime _modifiedDate;

        #endregion Declarations

        #region Properties

        public Guid TradingInformationId
        {
            get { return _tradingInformationId; }
            set { _tradingInformationId = value; }
        }

        public Currency Currency
        {
            get { return _currency; }
            set { _currency = value; }
        }

        public decimal TradingPrice
        {
            get { return _tradingPrice; }
            set { _tradingPrice = value; }
        }

        public string ReasonForTrading
        {
            get { return _reasonForTrading; }
            set { _reasonForTrading = value; }
        }

        public bool IsOwnerWillingToAddCash
        {
            get { return _isOwnerwillingToAddCash; }
            set { _isOwnerwillingToAddCash = value; }
        }

        public decimal CashAmountToAdd
        {
            get { return _cashAmountToAdd; }
            set { _cashAmountToAdd = value; }
        }

        public bool IsOwnerWillingToReceiveCash
        {
            get { return _isOwnerWillingToReceiveCash; }
            set { _isOwnerWillingToReceiveCash = value; }
        }

        public string TradeNotes
        {
            get { return _tradeNotes; }
            set { _tradeNotes = value; }
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
