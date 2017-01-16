using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameShop.Api.Contracts.Responses
{
    public class SearchGameResponse : ApiResponse
    {
        #region Constructors

        public SearchGameResponse()
        {

        }

        public SearchGameResponse(Result result)
            : base(result)
        {

        }

        public SearchGameResponse(Result result, string message)
            : base(result, message)
        {

        }

        #endregion Constructors
    }
}
