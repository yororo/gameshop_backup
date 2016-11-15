using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameShop.Contracts.Entities
{
    public class Account
    {
        #region Declarations

        private Guid _accountId;
        private string _username;
        private string _email;
        private bool _emailVerified;
        private string _passwordHash;
        private bool _isActive;
        private DateTime _createdDate;
        private DateTime _modifiedDate;

        #endregion Declarations

        #region Properties

        public Guid AccountId
        {
            get { return _accountId; }
            set { _accountId = value; }
        }

        public string Username
        {
            get { return _username; }
            set { _username = value; }
        }

        public string Email
        {
            get { return _email; }
            set { _email = value; }
        }

        public bool EmailVerified
        {
            get { return _emailVerified; }
            set { _emailVerified = value; }
        }

        public string PasswordHash
        {
            get { return _passwordHash; }
            set { _passwordHash = value; }
        }

        public bool IsActive
        {
            get { return _isActive; }
            set { _isActive = value; }
        }

        public DateTime CreatedDate
        {
            get { return _createdDate; }
            set { _createdDate = value; }
        }

        public DateTime ModifiedDate
        {
            get { return _modifiedDate; }
            set { _modifiedDate = value; }
        }

        #endregion Properties

        #region Constructors

        public Account()
        {
            _accountId = Guid.Empty;
            _username = string.Empty;
            _email = string.Empty;
            _emailVerified = false;
            _passwordHash = string.Empty;
            _isActive = false;
            _createdDate = DateTime.MaxValue;
            _modifiedDate = DateTime.MaxValue;
        }

        #endregion Constructors
    }
}
