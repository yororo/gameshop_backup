using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using GameShop.Api.Client.Advertisements;
using GameShop.Api.Client.Products;

namespace GameShop.Api.Client
{
    public class ApiClient
    {
        private static HttpClient _client = new HttpClient();

        public AdvertisementsApiClient Advertisements { get; set; }
        public ProductsApiClient Products { get; set; }
        
        public ApiClient(string baseUrl)
        {
            _client.BaseAddress = new Uri(baseUrl);
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            Advertisements = new AdvertisementsApiClient(_client);
            Products = new ProductsApiClient(_client);
        }

        /// <summary>
        /// Sets the bearer token to be included by the HTTP client for every API request.
        /// </summary>
        /// <param name="token">Bearer token.</param>
        public void SetBearerToken(string token)
        {
            _client.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
        }

        public async Task<string> GetTestGuid()
        {
            return await _client.GetStringAsync(Path.Combine("test", "guid"));
        }
    }
}