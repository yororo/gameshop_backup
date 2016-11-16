using GameShop.Contracts.Enumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameShop.Contracts.Entities
{
    public class User
    {
        #region Fields

        private Guid _userId;
        private Account _account;
        private Profile _profile;
        private DateTime _createdDate;
        private DateTime _modifiedDate;

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

        public Account Account
        {
            get
            {
                return _account;
            }

            set
            {
                _account = value;
            }
        }

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

        /// <summary>
        /// Default constructor initialization.
        /// </summary>
        public User()
        {
            UserId = Guid.Empty;
            Account = new Account();
            Profile = new Profile();
            CreatedDate = DateTime.MaxValue;
            ModifiedDate = DateTime.MaxValue;
        }

        #endregion
    }
}