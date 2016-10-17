using GameShop.Contracts.Enumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameShop.Contracts.Entities
{
    public class OperatingSystem
    {
        #region Fields

        private OS _OSType;
        private string _name;

        #endregion

        #region Properties

        public OS OSType
        {
            get
            {
                return _OSType;
            }

            set
            {
                _OSType = value;
            }
        }

        public string Name
        {
            get
            {
                return _name;
            }

            set
            {
                _name = value;
            }
        }

        #endregion

        #region Constructors

        public OperatingSystem()
        {
            OSType = OS.NotSpecified;
            Name = string.Empty;
        }

        #endregion
    }
}
