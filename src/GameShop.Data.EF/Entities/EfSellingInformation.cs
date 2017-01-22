using GameShop.Contracts.Enumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameShop.Data.EF.Entities
{
    internal class EfSellingInformation : EfEntity
    {
        public Guid Id { get; set; }
        public Currency Currency { get; set; }
        public decimal SellingPrice { get; set; }
        public string ReasonForSelling { get; set; }

        public Guid ProductId { get; set; }
        public EfProduct Product { get; set; }
    }
}
