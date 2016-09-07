using GameShop.Contracts.Enumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameShop.Contracts.Entities
{
    public class Feedback
    {
        public Guid id { get; set; }
        public string Details { get; set; }
        public Rating Rating { get; set; }
    }
}
