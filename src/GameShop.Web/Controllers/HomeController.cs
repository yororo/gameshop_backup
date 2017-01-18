using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using GameShop.Web.Options;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.IO;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;

namespace GameShop.Web.Controllers
{
    public class HomeController : Controller
    {
        private GameShopAuthorizationOptions _authOptions;
        private GameShopApiOptions _apiOptions;

        public HomeController(IOptions<GameShopApiOptions> apiOptions, IOptions<GameShopAuthorizationOptions> authOptions)
        {
            _authOptions = authOptions.Value;
            _apiOptions = apiOptions.Value;
        }

        public IActionResult Index()
        {
            return View();
        }

        [Authorize]
        public async Task<IActionResult> About()
        {
            using(var client = new HttpClient())
            {
                var token = await HttpContext.Authentication.GetTokenAsync("access_token");

                client.DefaultRequestHeaders.Add("Authorization", $"Bearer { token }");

                using (var response = await client.GetAsync("http://localhost:6001/test/admin"))
                {
                    var responseText = await response.Content.ReadAsStringAsync();

                    ViewData["Message"] = responseText;
                }
            }

            //ViewData["Message"] = "Your application description page.";

            return View();
        }
        
        public async Task<IActionResult> Contact()
        {
            using(HttpClient client = new HttpClient())
            {
                string token = await getAccessTokenAsync(client);
                // Set bearer token.
                client.DefaultRequestHeaders.Add("Authorization", $"Bearer { token }");

                using (var response = await client.GetAsync("http://localhost:6001/test/admin"))
                {
                    var responseText = await response.Content.ReadAsStringAsync();

                    ViewData["Message"] = responseText;
                }
            }

            return View();
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

        public IActionResult Error()
        {
            return View();
        }
    }
}
