using GameShop.Contracts.Enumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameShop.Contracts.Entities
{
    public class Profile
    {
        #region Properties

        public Guid Id { get; set; }

        public Name Name { get; set; }

        public Gender Gender { get; set; }

        public CivilStatus CivilStatus { get; set; }

        public DateTime Birthday { get; set; }

        public List<Address> Addresses { get; set; }

        public List<ContactInformation> ContactInformation { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime ModifiedDate { get; set; }

        #endregion Properties

        #region Constructors

        /// <summary>
        /// Default constructor.
        /// </summary>
        public Profile()
        {
            Id = Guid.Empty;
            Name = new Name();
            Gender = Gender.Unspecified;
            CivilStatus = CivilStatus.Unspecified;
            Birthday = DateTime.MaxValue;
            Addresses = new List<Address>();
            ContactInformation = new List<ContactInformation>();
            CreatedDate = DateTime.MaxValue;
            ModifiedDate = DateTime.MaxValue;
        }

        #endregion Constructors
    }
}
