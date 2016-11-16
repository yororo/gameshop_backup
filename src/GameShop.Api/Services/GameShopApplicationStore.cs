using GameShop.Api.Contracts.Entities;
using GameShop.Contracts.Entities;
using OpenIddict;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;

namespace GameShop.Api.Services
{
    public class GameShopApplicationStore : IOpenIddictApplicationStore<Client>
    {
        public Task<string> CreateAsync(Client application, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<Client> FindByClientIdAsync(string identifier, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<Client> FindByIdAsync(string identifier, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<Client> FindByLogoutRedirectUri(string url, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<string> GetClientTypeAsync(Client application, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<string> GetDisplayNameAsync(Client application, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<string> GetHashedSecretAsync(Client application, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<string> GetRedirectUriAsync(Client application, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<string>> GetTokensAsync(Client application, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
