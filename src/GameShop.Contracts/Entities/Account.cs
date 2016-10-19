using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameShop.Contracts.Entities
{
    public class Account
    {
        private Guid _accountId;
        private string _username;
        private string _password;
        private DateTime _created;
        private DateTime _modified;

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

        public string Password
        {
            get { return _password; }
            set { _password = value; }
        }
        
        public DateTime Created
        {
            get { return _created; }
            set { _created = value; }
        }

        public DateTime Modified
        {
            get { return _modified; }
            set { _modified = value; }
        }
    }
}
