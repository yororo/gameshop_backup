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
        private string _productName;
        private string _description;
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

        public string ProductName
        {
            get
            {
                return _productName;
            }

            set
            {
                _productName = value;
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

        public User CreatedBy
        {
            get
            {
                return _createdBy;
            }

            set
            {
                _createdBy = value;
            }
        }

        public DateTime CreatedDTTM
        {
            get
            {
                return _createdDTTM;
            }

            set
            {
                _createdDTTM = value;
            }
        }

        public User ModifiedBy
        {
            get
            {
                return _modifiedBy;
            }

            set
            {
                _modifiedBy = value;
            }
        }

        public DateTime ModifiedDTTM
        {
            get
            {
                return _modifiedDTTM;
            }

            set
            {
                _modifiedDTTM = value;
            }
        }
        #endregion

        #region Constructors
        public Product()
        {
            ProductId = Guid.Empty;
            ProductName = string.Empty;
            Description = string.Empty;
            CreatedBy = new User();
            CreatedDTTM = DateTime.MaxValue;
            ModifiedBy = new User();
            ModifiedDTTM = DateTime.MaxValue;
        }
        #endregion

        #region Private Methods

        #endregion

        #region Public Methods

        #endregion
    }
}
