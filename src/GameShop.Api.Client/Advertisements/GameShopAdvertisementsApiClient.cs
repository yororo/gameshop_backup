using System;
using System.IO;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

using GameShop.Api.Contracts.Constants;

namespace GameShop.Api.Client.Advertisements
{
    public class GameShopAdvertisementsApiClient
    {
        private readonly HttpClient _client;

        public GameShopAdvertisementsApiClient(HttpClient client)
        {
            _client = client;   
        }

        public async Task<string> GetAllGameAdvertisementsAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            return await _client.GetStringAsync(ApiEndpoints.GameAdvertisements);
        }

        public async Task<string> GetAllGameAdvertisementsDeepAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            return await _client.GetStringAsync($"{ApiEndpoints.GameAdvertisements}?deep=1");
        }

        public async Task<string> GetGameAdvertisementsByIdAsync(Guid id, CancellationToken cancellationToken = default(CancellationToken))
        {
            return await _client.GetStringAsync($"{ApiEndpoints.GameAdvertisements}?id={id}");
        }

        public async Task<string> GetGameAdvertisementsByFriendlyIdAsync(string friendlyId, CancellationToken cancellationToken = default(CancellationToken))
        {
            return await _client.GetStringAsync($"{ApiEndpoints.GameAdvertisements}?fid={friendlyId}");
        }

        public async Task<string> GetGameAdvertisementsByTitleAsync(string title, CancellationToken cancellationToken = default(CancellationToken))
        {
            return await _client.GetStringAsync($"{ApiEndpoints.GameAdvertisements}?title={title}");
        }
    }
}