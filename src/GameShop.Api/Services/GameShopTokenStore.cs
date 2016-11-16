using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using OpenIddict;

namespace GameShop.Api.Services
{
    public class GameShopTokenStore : IOpenIddictTokenStore<string>
    {
        public Task<string> CreateAsync(string type, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<string> FindByIdAsync(string identifier, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task RevokeAsync(string token, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
