using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameShop.Contracts.Entities
{
    public class ComputerSpecification
    {
        public Guid Id { get; set; }
        public CPU CPU { get; set; }
        public RAM RAM { get; set; }
        public StorageDevice Storage { get; set; }
        public OperatingSystem OperatingSystem { get; set; }
    }
}
