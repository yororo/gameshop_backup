﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameShop.Data.EF.Entities
{
    internal class ContactInformation : Entity
    {
        public Guid ContactInformationId { get; set; }
        public string Email { get; set; }
        public string MobileNumber { get; set; }
    }
}