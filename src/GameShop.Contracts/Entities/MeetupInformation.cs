using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameShop.Contracts.Entities
{
    public class MeetupInformation
    {
        #region Properties

        public List<Address> MeetupLocations { get; set; }

        #endregion Properties

        #region Constructors
        
        public MeetupInformation()
        {
            MeetupLocations = new List<Address>();
        }

        #endregion Constructors
    }
}
