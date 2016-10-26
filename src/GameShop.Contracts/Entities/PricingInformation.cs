using GameShop.Contracts.Enumerations;
using System;

namespace GameShop.Contracts.Entities
{
    public class PricingInformation
    {
        #region Fields

        private Guid _pricingInformationId;
        private decimal _tradePrice;
        private decimal _salePrice;
        private Currency _currency;
        private AuditInformation _auditInformation;

        #endregion Fields

        #region Properties

        public Guid PricingInformationId
        {
            get
            {
                return _pricingInformationId;
            }

            set
            {
                _pricingInformationId = value;
            }
        }

        public decimal TradePrice
        {
            get
            {
                return _tradePrice;
            }

            set
            {
                _tradePrice = value;
            }
        }

        public decimal SalePrice
        {
            get
            {
                return _salePrice;
            }

            set
            {
                _salePrice = value;
            }
        }

        public Currency Currency
        {
            get
            {
                return _currency;
            }

            set
            {
                _currency = value;
            }
        }

        public AuditInformation AuditInformation
        {
            get
            {
                return _auditInformation;
            }

            set
            {
                _auditInformation = value;
            }
        }

        #endregion Properties

        #region Constructor

        public PricingInformation()
        {
            PricingInformationId = Guid.Empty;
            TradePrice = decimal.Zero;
            SalePrice = decimal.Zero;
            AuditInformation = new AuditInformation();
        }

        #endregion Constructor
    }
}