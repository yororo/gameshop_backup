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
        private string _normalizedUsername;
        private string _email;
        private string _normalizedEmail;
        private bool _emailVerified;
        private string _phoneNumber;
        private bool _phoneNumberVerified;
        private string _passwordHash;
        private bool _twoFactorEnabled;
        private bool _lockoutEnabled;
        private DateTimeOffset? _lockoutEnd;
        private int _accessFailedCount;
        private string _securityStamp;
        private string _concurrencyStamp;

        #endregion Declarations

        #region Properties

        /// <summary>
        /// Gets or sets the user name for this user.
        /// </summary>
        public string Username
        {
            get { return _username; }
            set { _username = value; }
        }

        /// <summary>
        /// Gets or sets the normalized user name for this user.
        /// </summary>
        public string NormalizedUsername
        {
            get { return _normalizedUsername; }
            set { _normalizedUsername = value; }
        }
        
        /// <summary>
        /// Gets or sets the email address for this user.
        /// </summary>
        public string Email
        {
            get { return _email; }
            set { _email = value; }
        }

        /// <summary>
        /// Gets or sets the email address for this user.
        /// </summary>
        public string NormalizedEmail
        {
            get { return _normalizedEmail; }
            set { _normalizedEmail = value; }
        }

        /// <summary>
        /// Gets or sets a flag indicating if a user has confirmed their email address.
        /// </summary>
        public bool EmailConfirmed
        {
            get { return _emailVerified; }
            set { _emailVerified = value; }
        }

        /// <summary>
        /// Gets or sets a telephone number for the user.
        /// </summary>
        public string PhoneNumber
        {
            get { return _phoneNumber; }
            set { _phoneNumber = value; }
        }

        /// <summary>
        /// Gets or sets a flag indicating if a user has confirmed their telephone address.
        /// </summary>
        public bool PhoneNumberConfirmed
        {
            get { return _phoneNumberVerified; }
            set { _phoneNumberVerified = value; }
        }
        
        /// <summary>
         /// Gets or sets a salted and hashed representation of the password for this user.
         /// </summary>
        public string PasswordHash
        {
            get { return _passwordHash; }
            set { _passwordHash = value; }
        }

        /// <summary>
        ///  Gets or sets a flag indicating if two factor authentication is enabled for this user.
        /// </summary>
        public bool TwoFactorEnabled
        {
            get { return _twoFactorEnabled; }
            set { _twoFactorEnabled = value; }
        }

        /// <summary>
        /// Gets or sets a flag indicating if the user could be locked out.
        /// </summary>
        public bool LockoutEnabled
        {
            get { return _lockoutEnabled; }
            set { _lockoutEnabled = value; }
        }

        /// <summary>
        /// Gets or sets the date and time, in UTC, when any user lockout ends.
        /// </summary>
        /// <remarks>
        /// A value in the past means the user is not locked out.
        /// </remarks>
        public DateTimeOffset? LockoutEnd
        {
            get { return _lockoutEnd; }
            set { _lockoutEnd = value; }
        }

        /// <summary>
        /// Gets or sets the number of failed login attempts for the current user.
        /// </summary>
        public int AccessFailedCount
        {
            get { return _accessFailedCount; }
            set { _accessFailedCount = value; }
        }

        /// <summary>
        /// A random value that must change whenever a users credentials change (password changed, login removed)
        /// </summary>
        public string SecurityStamp
        {
            get { return _securityStamp; }
            set { _securityStamp = value; }
        }

        /// <summary>
        /// A random value that must change whenever a user is persisted to the store.
        /// </summary>
        public string ConcurrencyStamp
        {
            get { return _concurrencyStamp; }
            set { _concurrencyStamp = value; }
        }

        #endregion Properties

        #region Constructors

        public Account()
        {
            _username = string.Empty;
            _email = string.Empty;
            _emailVerified = false;
            _passwordHash = string.Empty;
            _phoneNumber = string.Empty;
            _phoneNumberVerified = false;
            _lockoutEnabled = true;
            _accessFailedCount = 0;
        }

        #endregion Constructors
    }
}
