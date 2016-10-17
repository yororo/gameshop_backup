using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameShop.Contracts.API.Responses
{
    public abstract class ApiResponse
    {
        private Result _result;

        public Result Result
        {
            get { return _result; }
            set { _result = value; }
        }

    }
}
