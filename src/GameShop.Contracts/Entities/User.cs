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
        private List<Feedback> _feedbacks;

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

        public List<Feedback> Feedbacks
        {
            get
            {
                return _feedbacks;
            }
            set
            {
                _feedbacks = value;
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
            Feedbacks = new List<Feedback>();
        }

        #endregion
    }
}