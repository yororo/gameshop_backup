using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameShop.Contracts.API.Requests
{
    public abstract class APIRequest
    {
        #region Fields
        private int _requestId = 0;
        private DateTime _createdDTTM = DateTime.Now;
        #endregion

        #region Properties
        public int RequestId
        {
            get
            {
                return _requestId;
            }

            set
            {
                _requestId = value;
            }
        }

        public DateTime CreatedDTTM
        {
            get
            {
                return _createdDTTM;
            }

            set
            {
                _createdDTTM = value;
            }
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Default constructor initialization.
        /// </summary>
        public APIRequest()
        {
            
        }
        #endregion

        #region Private Methods
        
        #endregion

        #region Public Methods
        
        #endregion
    }
}
