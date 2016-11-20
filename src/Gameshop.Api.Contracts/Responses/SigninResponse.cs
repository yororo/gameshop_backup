using GameShop.Contracts.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameShop.Api.Contracts.Responses
{
    public class SigninResponse : ApiResponse
    {
        #region Properties
    
        public string AcessToken { get; set; }
        public Profile UserProfile { get; set; }

        #endregion Properties

        #region Constructors

        public SigninResponse()
        {

        }

        public SigninResponse(Result result)
            : base(result)
        {

        }

        public SigninResponse(Result result, string message)
            : base(result, message)
        {

        }

        #endregion Constructors
    }
}
