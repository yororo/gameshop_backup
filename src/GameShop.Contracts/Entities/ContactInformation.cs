using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameShop.Contracts.Entities
{
    public class ContactInformation
    {
        #region Fields

        private Guid _contactInformationId;
        private string _email;
        private string _contactNumber;
        private DateTime _createdDTTM;
        private DateTime _modifiedDTTM;
        private User _createdBy;
        private User _modifiedBy;

        #endregion

        #region Properties

        public Guid ContactInformationId
        {
            get
            {
                return _contactInformationId;
            }

            set
            {
                _contactInformationId = value;
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

        public DateTime CreatedDTTM
        {
            get { return _createdDTTM; }
            set { _createdDTTM = value; }
        }

        public DateTime ModifiedDTTM
        {
            get { return _modifiedDTTM; }
            set { _modifiedDTTM = value; }
        }

        public User CreatedBy
        {
            get { return _createdBy; }
            set { _createdBy = value; }
        }

        public User ModifiedBy
        {
            get { return _modifiedBy; }
            set { _modifiedBy = value; }
        }

        #endregion

        #region Constructors

        public ContactInformation()
        {
            ContactInformationId = Guid.Empty;
            Email = string.Empty;
            ContactNumber = string.Empty;
            CreatedDTTM = DateTime.MaxValue;
            ModifiedDTTM = DateTime.MaxValue;
            CreatedBy = new User();
            ModifiedBy = new User();
        }

        #endregion
    }
}
