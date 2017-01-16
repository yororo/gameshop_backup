using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameShop.Contracts.Entities
{
    public class Person
    {
        #region Properties

        public Profile Profile { get; set; }

        #endregion Properties

        #region Constructors

        /// <summary>
        /// Default constructor.
        /// </summary>
        public Person()
        {
            Profile = new Profile();
        }

        #endregion Constructors
    }
}
