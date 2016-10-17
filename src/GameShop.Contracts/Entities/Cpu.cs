using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameShop.Contracts.Entities
{
    public class CPU
    {
        #region Fields

        private string _name;
        private string _clockSpeed;
        private int _cores;

        #endregion

        #region Properties
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

        public string ClockSpeed
        {
            get
            {
                return _clockSpeed;
            }

            set
            {
                _clockSpeed = value;
            }
        }

        public int Cores
        {
            get
            {
                return _cores;
            }

            set
            {
                _cores = value;
            }
        }

        #endregion

        #region Constructors

        public CPU()
        {
            Name = string.Empty;
            ClockSpeed = string.Empty;
            Cores = 1;
        }

        #endregion
    }
}
