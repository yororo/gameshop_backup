using CryptoHelper;
using IdentityModel.Client;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace GameShop.Console
{
    public class App
    {
        private string _accessToken;
        private DiscoveryResponse _discovery;

        public App()
        {
            Start();
        }

        public async void Start()
        {
            string password = Crypto.HashPassword("secret");
            //_discovery = await DiscoveryClient.GetAsync("http://localhost:5000");
        }

        public async void GetClientToken()
        {
            // request token
            var tokenClient = new TokenClient(_discovery.TokenEndpoint, "client", "secret");
            var tokenResponse = await tokenClient.RequestClientCredentialsAsync("api1");

            if (tokenResponse.IsError)
            {
                System.Console.WriteLine(tokenResponse.Error);
                return;
            }

            _accessToken = tokenResponse.AccessToken;

            System.Console.WriteLine(tokenResponse.Json);

            CallToApi();
        }

        public async void GetUserToken()
        {
            _discovery = await DiscoveryClient.GetAsync("http://localhost:5000");

            // request token
            var tokenClient = new TokenClient(_discovery.TokenEndpoint, "ro.client", "secret");
            var tokenResponse = await tokenClient.RequestResourceOwnerPasswordAsync("user", "pass", "api1");

            if (tokenResponse.IsError)
            {
                System.Console.WriteLine(tokenResponse.Error);
                return;
            }

            _accessToken = tokenResponse.AccessToken;

            System.Console.WriteLine(tokenResponse.Json);
            System.Console.WriteLine("\n\n");

            CallToApi();
        }

        public async void CallToApi()
        {
            // call api
            var client = new HttpClient();
            client.SetBearerToken(_accessToken);

            var response = await client.GetAsync("http://localhost:5000/auth/test");
            if (!response.IsSuccessStatusCode)
            {
                System.Console.WriteLine(response.StatusCode);
            }

            var content = response.Content.ReadAsStringAsync().Result;
            System.Console.WriteLine(JArray.Parse(content));
        }
    }
}
