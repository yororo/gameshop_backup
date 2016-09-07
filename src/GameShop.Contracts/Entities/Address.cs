﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameShop.Contracts.Entities
{
    public class Address
    {
        public Guid Id { get; set; }
        public string Barangay { get; set; }
        public string Municipality { get; set; }
        public string City { get; set; }
        public string ZipCode { get; set; }
        public string Province { get; set; }
        public string Region { get; set; }
        public string Country { get; set; }
    }
}
