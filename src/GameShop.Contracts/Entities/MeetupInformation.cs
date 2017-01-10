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

        #endregion Properties
    }
}
