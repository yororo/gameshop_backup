using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http.Authentication;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Authorization;
using Auth0.AuthenticationApi;
using Auth0.AuthenticationApi.Models;
using GameShop.Api.Auth.Options;
using GameShop.Api.Contracts.Requests;
using Auth0.Core;
using Auth0.Core.Exceptions;
using GameShop.Api.Contracts.Responses;
using GameShop.Api.Contracts;
using Auth0.AuthenticationApi.Builders;
using Microsoft.Extensions.Configuration;

namespace GameShop.Api.Auth.Controllers
{
    public class AuthController : Controller
    {
        private IAuthenticationApiClient _auth0AuthenticationApiClient;
        private Auth0Options _auth0Options;
        private IConfiguration _configuration;

        public AuthController(IAuthenticationApiClient authenticationApiClient, IOptions<Auth0Options> auth0Options, IConfiguration configuration)
        {
            _auth0AuthenticationApiClient = authenticationApiClient;
            _auth0Options = auth0Options.Value;
            _configuration = configuration;
        }

        [HttpGet("signin")]
        public IActionResult Signin()

        {
            Uri authorizationUri = _auth0AuthenticationApiClient.BuildAuthorizationUrl()
                                                                .WithResponseType(AuthorizationResponseType.Code)
                                                                .WithClient(_auth0Options.ClientId)
                                                                .WithRedirectUrl(_auth0Options.CallbackUri)
                                                                .Build();
            
            return Redirect(authorizationUri.ToString());
        }

        [HttpGet("signin-auth0")]
        public async Task<IActionResult> SigninAuth0([FromQuery]string code, [FromQuery]string state = null)
        {
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
            return "Thou are authenticated. Hello master!";
        }
    }
}
