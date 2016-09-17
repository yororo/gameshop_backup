using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameShop.Contracts.API.Responses
{
    public class LoginResponse : APIResponse
    {
        #region Fields
        private string _token;
        private boolean _loginValid;
        #endregion

        #region Properties
        public string Token 
        { 
            get
            {
                return _token;
            }

            set
            {
                _token = value;
            } 
        }

        public boolean _LoginValid
        {
            get
            {
                return _loginValid;
            }

            set
            {
                _loginValid = value;
            }
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Default constructor initialization.
        /// </summary>
        public SearchAdResponse()
        {
            Token = string.Empty;
            LoginValid = false;
        }
        #endregion

        #region Private Methods
        
        #endregion

        #region Public Methods
        
        #endregion
    }
}
