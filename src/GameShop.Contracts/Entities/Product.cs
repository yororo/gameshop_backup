using GameShop.Contracts.Enumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameShop.Contracts.Entities
{
    public abstract class Product
    {
        #region Fields
        
        private Guid _id;
        // private Advertisement _advertisement;
        private string _name;
        private string _description;
        private SellingInformation _sellingInformation;
        private TradingInformation _tradingInformation;
        private ProductState _state;
        private DateTime _createdDate;
        private DateTime _modifiedDate;

        #endregion

        #region Properties

        public Guid Id
        {
            get
            {
                return _id;
            }

            set
            {
                _id = value;
            }
        }

        // public Advertisement Advertisement
        // {
        //     get
        //     {
        //         return _advertisement;
        //     }

        //     set
        //     {
        //         _advertisement = value;
        //     }
        // }

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

        public SellingInformation SellingInformation
        {
            get
            {
                return _sellingInformation;
            }
            set
            {
                _sellingInformation = value;
            }
        }

        public TradingInformation TradingInformation
        {
            get
            {
                return _tradingInformation;
            }
            set
            {
                _tradingInformation = value;
            }
        }

        public ProductState ProductState
        {
            get
            {
                return _state;
            }
            set
            {
                _state = value;
            }
        }

        public DateTime CreatedDate
        {
            get
            {
                return _createdDate;
            }
            set
            {
                _createdDate = value;
            }
        }

        public DateTime ModifiedDate
        {
            get
            {
                return _modifiedDate;
            }
            set
            {
                _modifiedDate = value;
            }
        }

        #endregion

        #region Constructors
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

        #endregion
    }
}
