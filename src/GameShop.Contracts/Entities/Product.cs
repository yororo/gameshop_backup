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
        private PricingInformation _pricingInformation;
        private DateTime _createdDTTM;
        private DateTime _modifiedDTTM;
        private User _createdBy;
        private User _modifiedBy;

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

        public DateTime CreatedDTTM
        {
            get { return _createdDTTM; }
            set { _createdDTTM = value; }
        }

        public DateTime ModifiedDTTM
        {
            get { return _modifiedDTTM; }
            set { _modifiedDTTM = value; }
        }

        public User CreatedBy
        {
            get { return _createdBy; }
            set { _createdBy = value; }
        }

        public User ModifiedBy
        {
            get { return _modifiedBy; }
            set { _modifiedBy = value; }
        }

        #endregion

        #region Constructors
        public Product()
        {
            ProductId = Guid.Empty;
            Name = string.Empty;
            Description = string.Empty;
            CreatedDTTM = DateTime.MaxValue;
            ModifiedDTTM = DateTime.MaxValue;
            CreatedBy = new User();
            ModifiedBy = new User();
        }

        #endregion
    }
}
