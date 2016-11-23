using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GameShop.Contracts.Entities;

namespace GameShop.Data.EF.Entities
{
    internal class Account : Entity
    {
        public Guid AccountId { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public bool EmailVerified { get; set; }
        public string PasswordHash { get; set; }
        public bool IsActive { get; set; }

        public Guid UserId { get; set; }
        public virtual User User { get; set; }
    }
}
