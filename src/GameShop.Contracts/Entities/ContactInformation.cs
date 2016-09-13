﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameShop.Contracts.Entities
{
    public class ContactInformation
    {
        #region Fields
        private Guid _contactInfoId;
        private string _email;
        private string _contactNumber;
        #endregion

        #region Properties
        public Guid ContactInfoId
        {
            get
            {
                return _contactInfoId;
            }

            set
            {
                _contactInfoId = value;
            }
        }

        public string Email
        {
            get
            {
                return _email;
            }

            set
            {
                _email = value;
            }
        }

        public string ContactNumber
        {
            get
            {
                return _contactNumber;
            }

            set
            {
                _contactNumber = value;
            }
        }
        #endregion

        #region Constructors
        public ContactInformation()
        {
            ContactInfoId = Guid.Empty;
            Email = string.Empty;
            ContactNumber = string.Empty;
        }
        #endregion

        #region Private Methods

        #endregion

        #region Public Methods

        #endregion
    }
}
