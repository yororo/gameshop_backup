using GameShop.Contracts.Enumerations;
using System;

namespace GameShop.Contracts.Entities
{
    public class PricingInformation
    {
        #region Fields

        private Guid _pricingInformationId;
        private decimal _price;
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

        public decimal Price
        {
            get
            {
                return _price;
            }

            set
            {
                _price = value;
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
            Price = decimal.Zero;
            this.Currency = Currency.PHP;
            AuditInformation = new AuditInformation();
        }

        #endregion Constructor
    }
}