using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using GameShop.Api.Client.Advertisements;
using GameShop.Api.Client.Products;

namespace GameShop.Api.Client
{
    public class GameShopApiClient
    {
        private readonly HttpClient _client;

        public GameShopAdvertisementsApiClient Advertisements { get; set; }
        public GameShopProductsApiClient Products { get; set; }
        
        public GameShopApiClient(HttpClient client, string uri)
        {
            _client = client;
            _client.BaseAddress = new Uri(uri);

            Advertisements = new GameShopAdvertisementsApiClient(_client);
            Products = new GameShopProductsApiClient(_client);
        }

        /// <summary>
        /// Sets the bearer token to be included by the HTTP client for every API request.
        /// </summary>
        /// <param name="token">Bearer token.</param>
        public void SetBearerToken(string token)
        {
            _client.DefaultRequestHeaders.Add("Authorization", $"Bearer { token }");
        }

        public async Task<string> GetTestGuid()
        {
            return await _client.GetStringAsync(Path.Combine("test", "guid"));
        }
    }
}