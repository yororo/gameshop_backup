using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameShop.Api.Contracts.Responses
{
    public class SignupResponse : ApiResponse
    {
        #region Constructors

        public SignupResponse()
        {

        }

        public SignupResponse(Result result)
            : base(result)
        {

        }

        public SignupResponse(Result result, string message)
            : base(result, message)
        {

        }

        #endregion Constructors
    }
}
