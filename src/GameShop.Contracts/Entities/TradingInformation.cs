using GameShop.Contracts.Enumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameShop.Contracts.Entities
{
    public class TradingInformation
    {
        #region Properties

        public Guid Id { get; set; }

        public Currency Currency { get; set; }

        public decimal TradingPrice { get; set; }

        public string ReasonForTrading { get; set; }

        public bool IsOwnerWillingToAddCash { get; set; }

        public decimal CashAmountToAdd { get; set; }

        public bool IsOwnerWillingToReceiveCash { get; set; }
        public decimal CashAmountToReceive { get; set; }

        public string TradeNotes { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime ModifiedDate { get; set; }

        #endregion Properties
        
        #region Constructors

        /// <summary>
        /// Default constructor.
        /// </summary>
        public TradingInformation()
        {
            Id = Guid.Empty;
            Currency = Currency.PHP;
            TradingPrice = decimal.Zero;
            ReasonForTrading = string.Empty;
            IsOwnerWillingToAddCash = false;
            CashAmountToAdd = decimal.Zero;
            IsOwnerWillingToReceiveCash = false;
            CashAmountToReceive = decimal.Zero;
            TradeNotes = string.Empty;
            CreatedDate = DateTime.MaxValue;
            ModifiedDate = DateTime.MaxValue;
        }    
        
        #endregion Constructors
    }
}
