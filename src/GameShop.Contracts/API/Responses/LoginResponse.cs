using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameShop.Contracts.API.Responses
{
    public class LoginResponse : ApiResponse
    {
        #region Fields

        private string _token;

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

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor initialization.
        /// </summary>
        public LoginResponse()
        {
            Token = string.Empty;
        }

        #endregion
    }
}
