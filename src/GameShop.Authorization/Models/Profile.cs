using GameShop.Contracts.Enumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GameShop.Contracts.Entities;

namespace GameShop.Authorization.Models
{
    public class Profile : Entity
    {
        public Guid Id { get; set; }
        public Salutation Salutation { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Suffix { get; set; }
        public Gender Gender { get; set; }
        public CivilStatus CivilStatus { get; set; }
        public DateTime Birthday { get; set; }
        public virtual ICollection<ProfileAddress> Addresses { get; set; }
        public virtual ICollection<ProfileContactInformation> ContactInformation { get; set; }

        public string UserId { get; set; }
        public virtual ApplicationUser User { get; set; }

        /// <summary>
        /// Constructor.
        /// </summary>
        public Profile()
        {
            Addresses = new List<ProfileAddress>();
            ContactInformation = new List<ProfileContactInformation>();
        }
    }
}
