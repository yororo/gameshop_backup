using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace GameShop.Console
{
    public class Program
    {
        public static void Main(string[] args) => MainAsync(args).GetAwaiter().GetResult();

        public static async Task MainAsync(string[] args)
        {
            var client = new HttpClient();

            //const string email = "bob@le-magnifique.com", password = "}s>EWG@f4g;_v7nB";

            //await CreateAccountAsync(client, email, password);

            var token = await GetTokenAsync(client, "jjj", "P@ssword123");
            System.Console.WriteLine("Access token: {0}", token);
            System.Console.WriteLine();

            var resource = await GetResourceAsync(client, token);
            System.Console.WriteLine("API response: {0}", resource);

            System.Console.ReadLine();
        }

        public static async Task CreateAccountAsync(HttpClient client, string email, string password)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, "http://localhost:5000/account/register");
            request.Content = new StringContent(JsonConvert.SerializeObject(new { email, password }), Encoding.UTF8, "application/json");

            var response = await client.SendAsync(request, HttpCompletionOption.ResponseContentRead);
            response.EnsureSuccessStatusCode();
        }

        public static async Task<string> GetTokenAsync(HttpClient client, string email, string password)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, "http://localhost:5000/connect/token");
            request.Content = new FormUrlEncodedContent(new Dictionary<string, string>
            {
                ["grant_type"] = "password",
                ["username"] = email,
                ["password"] = password,
                ["client_id"] = "1973"
            });

            var response = await client.SendAsync(request, HttpCompletionOption.ResponseContentRead);
            response.EnsureSuccessStatusCode();

            var payload = JObject.Parse(await response.Content.ReadAsStringAsync());
            if (payload["error"] != null)
            {
                throw new InvalidOperationException("An error occurred while retrieving an access token.");
            }

            return (string)payload["access_token"];
        }

        public static async Task<string> GetResourceAsync(HttpClient client, string token)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, "http://localhost:5000/test/admin");
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await client.SendAsync(request, HttpCompletionOption.ResponseContentRead);
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadAsStringAsync();
        }
    }
}
