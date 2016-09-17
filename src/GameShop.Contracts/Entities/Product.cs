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
<<<<<<< HEAD
=======
        private DateTime _created;
        private DateTime _modified;
>>>>>>> origin/master
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

        public DateTime Created
        {
            get
            {
                return _created;
            }

            set
            {
                _created = value;
            }
        }

        public DateTime Modified
        {
            get
            {
                return _modified;
            }

            set
            {
                _modified = value;
            }
        }
        #endregion

        #region Constructors
        public Product()
        {
            ProductId = Guid.Empty;
            ProductName = string.Empty;
            Description = string.Empty;
            Created = DateTime.MaxValue;
            Modified = DateTime.MaxValue;
        }
        #endregion

        #region Private Methods

        #endregion

        #region Public Methods

        #endregion
    }
}
