using System;
using System.IO;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

using GameShop.Contracts.Enumerations;

namespace GameShop.Api.Client.Products
{
    public class GameShopProductsApiClient
    {
        private readonly HttpClient _client;

        public GameShopProductsApiClient(HttpClient client)
        {
            _client = client;
        }
        
        public async Task<string> GetGameByIdAsync(Guid id, CancellationToken cancellationToken = default(CancellationToken))
        {
            return await _client.GetStringAsync($"games?id={id}");
        }

        public async Task<string> GetGamesByTitleAsync(string title, CancellationToken cancellationToken = default(CancellationToken))
        {
            return await _client.GetStringAsync($"games?title={title}");
        }

        public async Task<string> GetGamesByGenreAsync(GameGenre genre, CancellationToken cancellationToken = default(CancellationToken))
        {
            return await _client.GetStringAsync($"games?genre={genre}");
        }
    }
}