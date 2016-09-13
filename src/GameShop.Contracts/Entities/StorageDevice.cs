using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameShop.Contracts.Entities
{
    public class StorageDevice
    {
        #region Fields
        private string _capacity;
        #endregion

        #region Properties
        public string Capacity
        {
            get
            {
                return _capacity;
            }

            set
            {
                _capacity = value;
            }
        }
        #endregion

        #region Constructors
        public StorageDevice()
        {
            Capacity = string.Empty;
        }
        #endregion

        #region Private Methods

        #endregion

        #region Public Methods

        #endregion
    }
}
