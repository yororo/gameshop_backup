// using GameShop.Contracts.Enumerations;
// using System;
// using System.Collections.Generic;
// using System.Linq;
// using System.Threading.Tasks;
// using GameShop.Contracts.Entities;

// namespace GameShop.Data.EF.Entities
// {
//     internal class Profile : Entity
//     {
//         public Salutation Salutation { get; set; }
//         public string FirstName { get; set; }
//         public string MiddleName { get; set; }
//         public string LastName { get; set; }
//         public string Suffix { get; set; }
//         public Gender Gender { get; set; }
//         public CivilStatus CivilStatus { get; set; }
//         public DateTime Birthday { get; set; }
//         public virtual ICollection<ProfileAddress> Addresses { get; set; }
//         public virtual ICollection<ProfileContactInformation> ContactInformation { get; set; }


//         public Guid UserId { get; set; }
//         public virtual User User { get; set; }
//     }
// }
