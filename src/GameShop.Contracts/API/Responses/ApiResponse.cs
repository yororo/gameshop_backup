using GameShop.Contracts.Enumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameShop.Contracts.API.Responses
{
    public abstract class APIResponse
    {
        #region Fields
        private int _responseId = 0;
        private string _errorMessage = string.Empty;
        private APIResult _apiResult = APIResult.Default;
        private DateTime _createdDTTM = DateTime.Now;
        #endregion

        #region Properties
        public int ResponseId
        {
            get
            {
                return _responseId;
            }

            set
            {
                _responseId = value;
            }
        }

        public string ErrorMessage 
        { 
            get
            {
                return _errorMessage;
            }

            set
            {
                _errorMessage = value;
            } 
        }

        public APIResult APIResult
        {
            get
            {
                return _apiResult;
            }

            set
            {
                _apiResult = value;
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
        public APIResponse()
        {

        }
        #endregion

        #region Private Methods
        
        #endregion

        #region Public Methods
        
        #endregion
    }
}
