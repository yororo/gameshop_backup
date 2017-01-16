using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameShop.Contracts.Entities
{
    public class Account
    {
        #region Properties

        /// <summary>
        /// Gets or sets the user name for this user.
        /// </summary>
        public string Username { get; set; }
        
        /// <summary>
        /// Gets or sets the email address for this user.
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets a flag indicating if a user has confirmed their email address.
        /// </summary>
        public bool IsEmailConfirmed { get; set; }

        /// <summary>
        /// Gets or sets a telephone number for the user.
        /// </summary>
        public string PhoneNumber { get; set; }

        /// <summary>
        /// Gets or sets a flag indicating if a user has confirmed their telephone address.
        /// </summary>
        public bool IsPhoneNumberConfirmed { get; set; }

        /// <summary>
        ///  Gets or sets a flag indicating if two factor authentication is enabled for this user.
        /// </summary>
        public bool IsTwoFactorEnabled { get; set; }

        /// <summary>
        /// Gets or sets a flag indicating if the user could be locked out.
        /// </summary>
        public bool IsLockoutEnabled { get; set; }

        /// <summary>
        /// Gets or sets the date and time, in UTC, when any user lockout ends.
        /// </summary>
        /// <remarks>
        /// A value in the past means the user is not locked out.
        /// </remarks>
        public DateTimeOffset? LockoutEnd { get; set; }

        /// <summary>
        /// Gets or sets the number of failed login attempts for the current user.
        /// </summary>
        public int AccessFailedCount { get; set; }

        #endregion Properties

        #region Constructors

        /// <summary>
        /// Default constructor.
        /// </summary>
        public Account()
        {
            Username = string.Empty;
            Email = string.Empty;
            IsEmailConfirmed = false;
            PhoneNumber = string.Empty;
            IsPhoneNumberConfirmed = false;
            IsTwoFactorEnabled = false;
            IsLockoutEnabled = false;
            LockoutEnd = null;
            AccessFailedCount = 0;
        }

        #endregion Constructors
    }
}
