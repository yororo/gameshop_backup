using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using GameShop.Web.ViewModels;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Http.Authentication;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace GameShop.Web.Controllers
{
   public class AuthenticationController : Controller 
   {
        [HttpGet("~/login")]
        public string Login(string returnUrl = null) 
        {
            return "Login form";
        }

        [HttpPost("~/login")]
        public async Task<ActionResult> Login([FromBody]LoginViewModel model, string returnUrl = null) 
        {
            using(var client = new HttpClient())
            {
                var token = await getAccessTokenAsync(client);
                await HttpContext.Authentication.SignInAsync(OpenIdConnectDefaults.AuthenticationScheme, User, new AuthenticationProperties(){ });

                return Redirect("/");
            }
        }
        
        private async Task<string> getAccessTokenAsync(HttpClient client)
        {
            var result = await client.PostAsync("http://localhost:5000/connect/token",
             new FormUrlEncodedContent(new[]{
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

        [HttpGet("~/signin")]
        public ActionResult SignIn() {
            // Instruct the OIDC client middleware to redirect the user agent to the identity provider.
            // Note: the authenticationType parameter must match the value configured in Startup.cs
            return new ChallengeResult(OpenIdConnectDefaults.AuthenticationScheme, new AuthenticationProperties {
                RedirectUri = "/"
            });
        }

        [HttpGet("~/signout"), HttpPost("~/signout")]
        public async Task SignOut() {
            // Instruct the cookies middleware to delete the local cookie created when the user agent
            // is redirected from the identity provider after a successful authorization flow.
            await HttpContext.Authentication.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            // Instruct the OpenID Connect middleware to redirect the user agent to the identity provider to sign out.
            await HttpContext.Authentication.SignOutAsync(OpenIdConnectDefaults.AuthenticationScheme);
        }
    }
}