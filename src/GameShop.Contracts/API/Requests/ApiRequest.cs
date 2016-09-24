using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameShop.Contracts.API.Requests
{
    public abstract class ApiRequest
    {
        private Guid _requestId;

        public Guid RequestId
        {
            get { return _requestId; }
            set { _requestId = value; }
        }

        public ApiRequest()
        {
            _requestId = Guid.Empty;
        }
    }
}
