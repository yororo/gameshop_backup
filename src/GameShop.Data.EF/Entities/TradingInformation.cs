using GameShop.Contracts.Enumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameShop.Data.EF.Entities
{
    internal class TradingInformation : Entity
    {
        public Guid Id { get; set; }
        public Currency Currency { get; set; }
        public decimal TradingPrice { get; set; }
        public string ReasonForSelling { get; set; }
        public bool IsOwnerWillingToAddCash { get; set; }
        public decimal CashAmountToAdd { get; set; }
        public bool IsOwnerWillingToReceiveCash { get; set; }
        public string TradeNotes { get; set; }
    }
}
