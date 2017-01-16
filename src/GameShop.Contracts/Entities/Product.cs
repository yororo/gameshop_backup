﻿using GameShop.Contracts.Enumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameShop.Contracts.Entities
{
    public abstract class Product
    {
        #region Properties

        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public SellingInformation SellingInformation { get; set; }

        public TradingInformation TradingInformation { get; set; }

        public ProductState ProductState { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime ModifiedDate { get; set; }

        #endregion Properties

        #region Constructors

        /// <summary>
        /// Default constructor.
        /// </summary>
        public Product()
        {
            Id = Guid.Empty;
            Name = string.Empty;
            Description = string.Empty;
            SellingInformation = new SellingInformation();
            TradingInformation = new TradingInformation();
            CreatedDate = DateTime.MaxValue;
            ModifiedDate = DateTime.MaxValue;
        }

        #endregion Constructors
    }
}
