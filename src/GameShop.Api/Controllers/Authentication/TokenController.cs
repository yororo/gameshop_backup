using Auth0.AuthenticationApi;
using Auth0.AuthenticationApi.Models;
using GameShop.Api.Contracts;
using GameShop.Api.Contracts.Requests;
using GameShop.Api.Contracts.Responses;
using GameShop.Api.Options;
using GameShop.Api.Services.Interfaces;
using GameShop.Contracts.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameShop.Api.Controllers
{
    [Route("token")]
    public class TokenController : Controller
    {
        private IAuthenticationApiClient _auth0AuthenticationApiClient;
        private Auth0Options _auth0Options;
        private IConfiguration _configuration;

        public TokenController(IAuthenticationApiClient authenticationApiClient,
                                IOptions<Auth0Options> auth0Options, 
                                IConfiguration configuration)
        {
            _auth0AuthenticationApiClient = authenticationApiClient;
            _auth0Options = auth0Options.Value;
            _configuration = configuration;
        }

        [HttpGet]
        public async Task<IActionResult> GenerateAccessToken([FromQuery]string redirectUri, [FromQuery]string code, [FromQuery]string state = null)
        {
            var request = new ExchangeCodeRequest();
            request.ClientId = _auth0Options.ClientId;
            request.ClientSecret = _configuration["GameShopApiClientSecret"]; // Get client secret. This is configurable through VS 2015 tools.
            request.AuthorizationCode = code;
            request.RedirectUri = redirectUri;

            // Exchange the code receive from by the client app to an access token. 
            // This access token will be passed in all succeeding client requests in the Bearer header to be used for authorization.
            AccessToken accessToken = await _auth0AuthenticationApiClient.ExchangeCodeForAccessTokenAsync(request);
            accessToken.ExpiresIn = (int)DateTime.UtcNow.AddMinutes(60).ToUnixEpochDate();

            return Ok(accessToken);
        }
    }
}
