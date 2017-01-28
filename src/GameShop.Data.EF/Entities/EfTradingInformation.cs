using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using GameShop.Contracts.Enumerations;
using GameShop.Data.EF.Entities.Products;

namespace GameShop.Data.EF.Entities
{
    internal class EfTradingInformation : EfEntity
    {
        public Guid Id { get; set; }
        public Currency Currency { get; set; }
        public decimal TradingPrice { get; set; }
        public string ReasonForTrading { get; set; }
        public bool IsOwnerWillingToAddCash { get; set; }
        public decimal CashAmountToAdd { get; set; }
        public bool IsOwnerWillingToReceiveCash { get; set; }
        public decimal CashAmountToReceive { get; set; }
        public string TradeNotes { get; set; }

        public Guid ProductId { get; set; }
        public EfProduct Product { get; set; }
    }
}
