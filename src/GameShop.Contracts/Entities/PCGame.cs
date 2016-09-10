using GameShop.Contracts.Enumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameShop.Contracts.Entities
{
    public class PCGame : Game
    {
        #region Fields
        private ComputerSpecification _systemRequirements;
        #endregion

        #region Properties
        public ComputerSpecification SystemRequirements
        {
            get
            {
                return _systemRequirements;
            }

            set
            {
                _systemRequirements = value;
            }
        }
        #endregion

        #region Constructors
        public PCGame()
        {
            SystemRequirements = new ComputerSpecification();
        }
        #endregion

        #region Private Methods

        #endregion

        #region Public Methods

        #endregion
    }
}
