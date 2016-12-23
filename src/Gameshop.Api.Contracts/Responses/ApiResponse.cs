using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameShop.Api.Contracts.Responses
{
    public class ApiResponse
    {
        #region Properties
        
        public Result Result { get; set; }
        public string Description { get; set; }
        public IEnumerable<string> ErrorDescriptions { get; set; } = Enumerable.Empty<string>();

        #endregion Properties

        #region Constructors

        public ApiResponse(Result result, string message, IEnumerable<string> errorDescriptions)
            : this(result, message)
        {
            ErrorDescriptions = errorDescriptions;
        }

        public ApiResponse(Result result, string message)
        {
            Result = result;
            Description = message;
        }

        public ApiResponse(Result result)
            : this(result, string.Empty)
        {
        }

        public ApiResponse()
            : this(Result.Unknown, string.Empty)
        {
        }

        #endregion Constructors
    }
}
