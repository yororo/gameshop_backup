using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameShop.Api.Contracts.Responses
{
    public class AccessTokenResponse : ApiResponse
    {
        #region Properties

        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
        public long ExpiresIn { get; set; }

        #endregion Properties

        #region Constructors

        public AccessTokenResponse(Result result, string message)
        {
            Result = result;
            Message = message;
        }

        public AccessTokenResponse(Result result)
            : this(result, string.Empty)
        {
        }

        public AccessTokenResponse()
            : this(Result.Unknown, string.Empty)
        {
        }

        #endregion Constructors
    }
}
