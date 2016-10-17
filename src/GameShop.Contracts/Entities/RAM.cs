using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameShop.Contracts.Entities
{
    public class RAM
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

        public RAM()
        {
            Capacity = string.Empty;
        }

        #endregion
    }
}
