using System.Net.Http;

namespace GameShop.Api.Client.Products
{
    public class ProductsApiClient
    {
        private static HttpClient _client;

        public GamesApiClient Games { get; set; }
        public GameConsolesApiClient GameConsoles { get; set; }

        public ProductsApiClient(HttpClient client)
        {
            _client = client;
            Games = new GamesApiClient(client);
            GameConsoles = new GameConsolesApiClient(client);
        }
    }
}