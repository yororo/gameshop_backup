using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameShop.Contracts.Entities
{
    public class AuditInformation
    {
        #region Fields

        private DateTime _createdDTTM;
        private DateTime _modifiedDTTM;
        private User _createdBy;
        private User _modifiedBy;

        #endregion Fields

        #region Properties

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

        #endregion Properties

        #region Constructors

        public AuditInformation()
        { }

        public AuditInformation(User createdBy, DateTime createdDTTM)
        {
            this.CreatedBy = createdBy;
            this.CreatedDTTM = createdDTTM;
        }

        #endregion Constructors
    }
}