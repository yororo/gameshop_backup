using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameShop.Contracts.Entities
{
    public class Person
    {
        #region Fields
        
        private Profile _profile;

        #endregion

        #region Properties

        public Profile Profile
        {
            get
            {
                return _profile;
            }

            set
            {
                _profile = value;
            }
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor initialization.
        /// </summary>
        public Person()
        {
            _profile = new Profile();
        }

        #endregion
    }
}
