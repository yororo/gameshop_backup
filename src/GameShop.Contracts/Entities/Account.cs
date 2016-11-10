using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameShop.Contracts.Entities
{
    public class Account
    {
        #region Declarations

        private string _username;
        private string _email;
        private string _password;
        private DateTime _createdDate;
        private DateTime _modifiedDate;

        #endregion Declarations

        #region Properties

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

        public string Password
        {
            get { return _password; }
            set { _password = value; }
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
            Username = string.Empty;
            Email = string.Empty;
            Password = string.Empty;
            CreatedDate = DateTime.MaxValue;
            ModifiedDate = DateTime.MaxValue;
        }

        #endregion Constructors
    }
}
