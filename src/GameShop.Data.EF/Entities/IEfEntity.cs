using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameShop.Data.EF.Entities
{
    internal interface IEfEntity
    {
        DateTime? CreatedDate { get; set; }
        DateTime? ModifiedDate { get; set; }
    }
}
