using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameShop.Contracts.Entities
{
    public class MeetupInformation
    {
        #region Declarations

        private Guid _meetupInformationId;
        private List<Address> _meetupLocations;
        private DateTime _createdDate;
        private DateTime _modifiedDate;

        #endregion Declarations

        #region Properties

        public Guid MeetupInformationId
        {
            get
            {
                return _meetupInformationId;
            }
            set
            {
                _meetupInformationId = value;
            }
        }

        public List<Address> MeetupLocations
        {
            get
            {
                return _meetupLocations;
            }
            set
            {
                _meetupLocations = value;
            }
        }

        public DateTime CreatedDate
        {
            get
            {
                return _createdDate;
            }
            set
            {
                _createdDate = value;
            }
        }

        public DateTime ModifiedDate
        {
            get
            {
                return _modifiedDate;
            }
            set
            {
                _modifiedDate = value;
            }
        }


        #endregion Properties
    }
}
