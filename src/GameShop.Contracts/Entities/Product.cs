using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameShop.Contracts.Entities
{
    public abstract class Product
    {
        #region Fields

        private Guid _productId;
        private string _name;
        private string _description;
        private string _reasonForSelling;
        private bool _isForTrade;
        private bool _isForSale;
        private PricingInformation _pricingInformation;
        private AuditInformation _auditInformation;

        #endregion

        #region Properties

        public Guid ProductId
        {
            get
            {
                return _productId;
            }

            set
            {
                _productId = value;
            }
        }

        public string Name
        {
            get
            {
                return _name;
            }

            set
            {
                _name = value;
            }
        }

        public string Description
        {
            get
            {
                return _description;
            }

            set
            {
                _description = value;
            }
        }

        public string ReasonForSelling
        {
            get
            {
                return _reasonForSelling;
            }

            set
            {
                _reasonForSelling = value;
            }
        }

        public bool IsForTrade
        {
            get
            {
                return _isForTrade;
            }

            set
            {
                _isForTrade = value;
            }
        }

        public bool IsForSale
        {
            get
            {
                return _isForSale;
            }

            set
            {
                _isForSale = value;
            }
        }

        public PricingInformation PricingInformation
        {
            get
            {
                return _pricingInformation;
            }
            set
            {
                _pricingInformation = value;
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

        #endregion

        #region Constructors
        public Product()
        {
            ProductId = Guid.Empty;
            Name = string.Empty;
            Description = string.Empty;
            IsForTrade = false;
            _isForSale = false;
            AuditInformation = new AuditInformation();
        }

        #endregion
    }
}
