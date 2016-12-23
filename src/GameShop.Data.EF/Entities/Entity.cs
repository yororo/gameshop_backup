using GameShop.Data.EF.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameShop.Data.EF.Entities
{
    internal abstract class Entity<TId> : IEntity<TId>
    {
        public TId Id { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }

    internal abstract class Entity : Entity<Guid>
    {

    }
}
