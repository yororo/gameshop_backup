using GameShop.Api.Contracts;
using GameShop.Api.Contracts.Requests;
using GameShop.Api.Contracts.Responses;
using GameShop.Api.Options;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace GameShop.Api.Controllers
{
    [Route("auth")]
    public class AuthController : Controller
    {
        #region Declarations

        private Auth0Options _auth0Options;

        #endregion Declarations

        #region Constructors

        public AuthController(IOptions<Auth0Options> auth0Options)
        {
            _auth0Options = auth0Options.Value;
        }

        #endregion Constructors

        [HttpGet("login")]
        public async Task<IActionResult> Login(string returnUrl)
        {
            var uriBuilder = new StringBuilder(@"https://gameshop.auth0.com/authorize");
            uriBuilder.AppendFormat(@"?response_type={0}", WebUtility.UrlEncode(@"code"));
            uriBuilder.AppendFormat(@"&client_id={0}", WebUtility.UrlEncode(_auth0Options.ClientId));
            uriBuilder.AppendFormat(@"&redirect_uri={0}", WebUtility.UrlEncode(returnUrl));

            return Redirect(uriBuilder.ToString());
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest loginRequest)
        {
            var uriBuilder = new StringBuilder(@"https://gameshop.auth0.com/authorize");
            uriBuilder.AppendFormat(@"?response_type={0}", WebUtility.UrlEncode(@"code"));
            uriBuilder.AppendFormat(@"&client_id={0}", WebUtility.UrlEncode(_auth0Options.ClientId));
            uriBuilder.AppendFormat(@"&redirect_uri={0}", WebUtility.UrlEncode(@"http://localhost:5000/auth/callback"));

            return Redirect(uriBuilder.ToString());
        }

        [HttpGet("callback")]
        public async Task<IActionResult> Callback(string code)
        {
            var quesryString = HttpContext.Request.QueryString;

            if(!string.IsNullOrEmpty(code))
            {
                return Ok(code);
            }

            return Unauthorized();
        }

        [Authorize]
        [HttpGet("logout")]
        public async Task<IActionResult> Logout(string returnUrl)
        {
            var uriBuilder = new StringBuilder(@"https://gameshop.auth0.com/v2/logout");
            uriBuilder.AppendFormat(@"?returnTo={0}", WebUtility.UrlEncode(returnUrl));
            uriBuilder.AppendFormat(@"&client_id={1}", WebUtility.UrlEncode(_auth0Options.ClientId));

            return Redirect(uriBuilder.ToString());
        }
    }
}
