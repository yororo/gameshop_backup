using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameShop.Api.Contracts.Requests
{
    public class ApiRequest
    {
        public Guid RequestId { get; set; }

        public ApiRequest()
        {
            RequestId = Guid.NewGuid();
        }
    }
}
