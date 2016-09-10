using GameShop.Contracts.Enumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameShop.Contracts.Entities
{
    public class User : Person
    {
        #region Fields
        private Guid _userId;
        private UserType _userType;
        #endregion

        #region Properties
        public Guid UserId
        {
            get
            {
                return _userId;
            }

            set
            {
                _userId = value;
            }
        }

        public UserType UserType
        {
            get
            {
                return _userType;
            }

            set
            {
                _userType = value;
            }
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Default constructor initialization.
        /// </summary>
        public User()
        {
            UserId = Guid.Empty;
            UserType = UserType.Public;
            
        }
        #endregion

        #region Private Methods

        #endregion

        #region Public Methods

        #endregion
    }
}