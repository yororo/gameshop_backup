using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameShop.Authorization.Models
{
    public interface IEntity
    {
        DateTime? CreatedDate { get; set; }
        DateTime? ModifiedDate { get; set; }
    }
}
