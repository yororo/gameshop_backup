using GameShop.Api.Contracts.Requests;
using GameShop.Web.Options;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
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

        [HttpGet]
        public async Task<IActionResult> Signin()
        {
            return View();
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
                string responseText = await response.Content.ReadAsStringAsync();

                // Add the tokens to cookies to be used as Bearer token in succeeding requests to GameShop API.
            }

            // TODO: Add validation in Home screen.
            return Redirect("http://localhost:5002");
        }
    }
}
