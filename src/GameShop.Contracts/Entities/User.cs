using GameShop.Contracts.Enumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameShop.Contracts.Entities
{
    public class User : Person
    {
        #region Properties

        public Guid Id { get; set; }

        public Account Account { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime ModifiedDate { get; set; }

        #endregion Properties

        #region Constructors

        /// <summary>
        /// Default constructor initialization.
        /// </summary>
        public User()
        {
            Id = Guid.Empty;
            Account = new Account();
            CreatedDate = DateTime.MaxValue;
            ModifiedDate = DateTime.MaxValue;
        }

        #endregion Constructors
    }
}