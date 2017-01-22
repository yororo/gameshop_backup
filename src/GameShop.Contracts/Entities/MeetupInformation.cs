using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameShop.Contracts.Entities
{
    public class MeetupInformation
    {
        #region Properties

        public string ContactNumber { get; set; }
        public List<MeetupLocation> MeetupLocations { get; set; }
        public string Notes { get; set; }

        #endregion Properties

        #region Constructors
        
        public MeetupInformation()
        {
            ContactNumber = string.Empty;
            MeetupLocations = new List<MeetupLocation>();
            Notes = string.Empty;
        }

        #endregion Constructors
    }
}
