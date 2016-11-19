using GameShop.Api.Contracts.Requests;
using GameShop.Web.Options;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace GameShop.Web.Controllers
{
    [Route("auth")]
    public class AuthController : Controller
    {
        private Auth0Options _auth0Options;

        public AuthController(IOptions<Auth0Options> auth0Options)
        {
            _auth0Options = auth0Options.Value;
        }

        [HttpGet("login")]
        public async Task<IActionResult> Login()
        {
            return View();
        }

        [HttpGet("logout")]
        public async Task<IActionResult> Logout()
        {
            HttpClient client = new HttpClient();
            client.SetBearerToken(HttpContext.Request.Cookies["bearer"]);

            using (var response = await client.GetAsync("http://localhost:5000/auth/logout?returnUrl=http://localhost:5002"))
            {
                string json = await response.Content.ReadAsStringAsync();
                
                // Add the tokens to cookies to be used as Bearer token in succeeding requests to GameShop API.
                HttpContext.Response.Cookies.Delete("bearer");
                HttpContext.Response.Cookies.Delete("access_token");

                return RedirectToAction("Index", "Home");
            }
        }

        [HttpGet("signin-auth0")]
        public async Task<IActionResult> SigninAuth0([FromQuery]string code, [FromQuery]string state = null)
        {
            AccessTokenRequest request = new AccessTokenRequest();
            request.AuthorizationCode = code;
            request.State = state;

            HttpClient client = new HttpClient();
            using (var response = await client.GetAsync("http://localhost:5000/token?redirectUri=http://localhost:5002&code=" + code))
            {
                string json = await response.Content.ReadAsStringAsync();

                Dictionary<string, string> values = JsonConvert.DeserializeObject<Dictionary<string, string>>(json);
                
                // Add the tokens to cookies to be used as Bearer token in succeeding requests to GameShop API.
                HttpContext.Response.Cookies.Append("bearer", values["id_token"], new Microsoft.AspNetCore.Http.CookieOptions() { HttpOnly = true });
                HttpContext.Response.Cookies.Append("access_token", values["access_token"], new Microsoft.AspNetCore.Http.CookieOptions() { HttpOnly = true });
            }

            // TODO: Add validation in Home screen.
            return Redirect("http://localhost:5002");
        }
    }
}
