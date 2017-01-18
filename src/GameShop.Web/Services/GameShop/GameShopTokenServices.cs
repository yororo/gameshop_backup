using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using GameShop.Web.Services.GameShop.Interfaces;
using Newtonsoft.Json;

namespace GameShop.Web.Services.GameShop
{
    public class GameShopTokenServices : IGameShopTokenServices
    {
        public string GetAccessToken(string username, string password)
        {
            throw new NotImplementedException();
        }

        private async Task<string> getAccessTokenAsync(HttpClient client)
        {
            var result = await client.PostAsync("http://localhost:5000/connect/token", new FormUrlEncodedContent(new[]{
                new KeyValuePair<string, string>("client_id", "GameShop.Web"),
                new KeyValuePair<string, string>("client_secret", "secret_secret_secret"),
                new KeyValuePair<string, string>("grant_type", "password"),
                new KeyValuePair<string, string>("username", "jj@gmail.com"),
                new KeyValuePair<string, string>("password", "Password!23")
            }));

            var response = await result.Content.ReadAsStringAsync();
            dynamic json = JsonConvert.DeserializeObject(response);

            return json.access_token;
        }
    }
}