using Auth0.AuthenticationApi;
using Auth0.AuthenticationApi.Models;
using GameShop.Api.Contracts;
using GameShop.Api.Contracts.Requests;
using GameShop.Api.Contracts.Responses;
using GameShop.Api.Options;
using GameShop.Api.Services.Interfaces;
using GameShop.Contracts.Entities;
using GameShop.Data.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameShop.Api.Controllers
{
    [Route("auth")]
    public class AuthenticationController : Controller
    {
        private IAuthenticationApiClient _auth0AuthenticationApiClient;
        private Auth0Options _auth0Options;
        private IConfiguration _configuration;

        public AuthenticationController(IAuthenticationApiClient authenticationApiClient, IOptions<Auth0Options> auth0Options, IConfiguration configuration)
        {
            _auth0AuthenticationApiClient = authenticationApiClient;
            _auth0Options = auth0Options.Value;
            _configuration = configuration;
        }

        [HttpGet("signin")]
        public IActionResult Signin()

        {
            Uri authorizationUri = _auth0AuthenticationApiClient.BuildAuthorizationUrl()
                                                                .WithResponseType(AuthorizationResponseType.Token)
                                                                .WithClient(_auth0Options.ClientId)
                                                                .WithRedirectUrl(_auth0Options.CallbackUri)
                                                                .Build();

            return Redirect(authorizationUri.ToString());
        }

        [HttpGet("signin-auth0")]
        public async Task<IActionResult> SigninAuth0([FromQuery]string code, [FromQuery]string state = null)
        {
            // Get token.
            return RedirectToRoute(new { controller = "Token", action = nameof(TokenController.GenerateAccessToken), @code = code, @state = state });
        }

        [Authorize]
        [HttpGet("signout")]
        public IActionResult Signout([FromQuery]string returnUrl)
        {
            Uri signoutUri = _auth0AuthenticationApiClient.BuildLogoutUrl()
                                                            .WithReturnUrl(returnUrl)
                                                            .WithValue("client_id", _auth0Options.ClientId)
                                                            .Build();

            return Redirect(signoutUri.ToString());
        }

        [Authorize]
        [HttpGet("test")]

        public string Test()
        {
            return "Thou art authenticated. Hello master!";
        }
    }
}
