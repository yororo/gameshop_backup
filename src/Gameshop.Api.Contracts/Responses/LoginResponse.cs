using GameShop.Contracts.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameShop.Api.Contracts.Responses
{
    public class LoginResponse : ApiResponse
    {
        #region Properties
    
        public string Token { get; set; }
        public Profile UserProfile { get; set; }

        #endregion Properties

        #region Constructors

        public LoginResponse()
        {

        }

        public LoginResponse(Result result)
            : base(result)
        {

        }

        public LoginResponse(Result result, string message)
            : base(result, message)
        {

        }

        #endregion Constructors
    }
}
