﻿/*
 * Licensed under the Apache License, Version 2.0 (http://www.apache.org/licenses/LICENSE-2.0)
 * See https://github.com/openiddict/openiddict-core for more information concerning
 * the license and the contributors participating to this project.
 */

using System.Linq;
using System.Threading.Tasks;
using AspNet.Security.OpenIdConnect.Extensions;
using AspNet.Security.OpenIdConnect.Server;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using GameShop.Authorization.Attributes;
using GameShop.Authorization.Models;
using GameShop.Authorization.ViewModels.Authorization;
using GameShop.Authorization.ViewModels.Shared;
using Microsoft.AspNetCore.Builder;
using AspNet.Security.OpenIdConnect.Primitives;
using OpenIddict.Core;
using OpenIddict.Models;
using System.Collections.Generic;
using GameShop.Authorization.Options;
using Microsoft.Extensions.Options;

namespace GameShop.Authorization
 {
    public class AuthorizationController : Controller 
    {
        private readonly OpenIddictApplicationManager<OpenIddictApplication> _applicationManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly GameShopOptions _options;

        public AuthorizationController(OpenIddictApplicationManager<OpenIddictApplication> applicationManager,
                                        SignInManager<ApplicationUser> signInManager,
                                        UserManager<ApplicationUser> userManager,
                                        IOptions<GameShopOptions> options) 
        {
            _applicationManager = applicationManager;
            _signInManager = signInManager;
            _userManager = userManager;
            _options = options.Value;
        }

        #region Authorization code, implicit and implicit flows
        //Note: to support interactive flows like the code flow,
        //you must provide your own authorization endpoint action:

        [Authorize(ActiveAuthenticationSchemes = OpenIdConnectServerDefaults.AuthenticationScheme), HttpGet("~/connect/authorize")]
        public async Task<IActionResult> Authorize() 
        {
            OpenIdConnectRequest request = HttpContext.GetOpenIdConnectRequest();

            // Retrieve the application details from the database.
            var application = await _applicationManager.FindByClientIdAsync(request.ClientId, HttpContext.RequestAborted);
            if (application == null) 
            {
                return View("Error", new ErrorViewModel 
                {
                    Error = OpenIdConnectConstants.Errors.InvalidClient,
                    ErrorDescription = "Details concerning the calling client application cannot be found in the database"
                });
            }

            // Flow the request_id to allow OpenIddict to restore
            // the original authorization request from the cache.
            return View(new AuthorizeViewModel 
            {
                ApplicationName = application.DisplayName,
                RequestId = request.RequestId,
                Scope = request.Scope
            });
        }

        [Authorize(ActiveAuthenticationSchemes = OpenIdConnectServerDefaults.AuthenticationScheme), FormValueRequired("submit.Accept")]
        [HttpPost("~/connect/authorize"), ValidateAntiForgeryToken]
        public async Task<IActionResult> Accept() 
        {
            OpenIdConnectRequest request = HttpContext.GetOpenIdConnectRequest();

            // Retrieve the profile of the logged in user.
            var user = await _userManager.GetUserAsync(User);
            if (user == null) 
            {
                return View("Error", new ErrorViewModel 
                {
                    Error = OpenIdConnectConstants.Errors.ServerError,
                    ErrorDescription = "An internal error has occurred"
                });
            }

            // Create a new authentication ticket.
            var ticket = await createTicketAsync(request, user);

            // Returning a SignInResult will ask OpenIddict to issue the appropriate access/identity tokens.
            return SignIn(ticket.Principal, ticket.Properties, ticket.AuthenticationScheme);
        }

        [Authorize(ActiveAuthenticationSchemes = OpenIdConnectServerDefaults.AuthenticationScheme), FormValueRequired("submit.Deny")]
        [HttpPost("~/connect/authorize"), ValidateAntiForgeryToken]
        public IActionResult Deny() 
        {
            // Notify OpenIddict that the authorization grant has been denied by the resource owner
            // to redirect the user agent to the client application using the appropriate response_mode.
            return Forbid(OpenIdConnectServerDefaults.AuthenticationScheme);
        }

        // Note: the logout action is only useful when implementing interactive
        // flows like the authorization code flow or the implicit flow.
        [HttpGet("~/connect/logout")]
        public IActionResult InteractiveLogout() 
        {
            OpenIdConnectRequest request = HttpContext.GetOpenIdConnectRequest();

            // Flow the request_id to allow OpenIddict to restore
            // the original logout request from the distributed cache.
            return View(new LogoutViewModel 
            {
                RequestId = request.RequestId
            });
        }

        [HttpPost("~/connect/logout"), ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout() 
        {
            // Ask ASP.NET Core Identity to delete the local and external cookies created
            // when the user agent is redirected from the external identity provider
            // after a successful authentication flow (e.g Google or Facebook).
            await _signInManager.SignOutAsync();

            // Returning a SignOutResult will ask OpenIddict to redirect the user agent
            // to the post_logout_redirect_uri specified by the client application.
            return SignOut(OpenIdConnectServerDefaults.AuthenticationScheme);
        }
        #endregion

        #region Password and refresh token flows
        // Note: to support non-interactive flows like password,
        // you must provide your own token endpoint action:

        [HttpPost("~/connect/token"), Produces("application/json")]
        public async Task<IActionResult> Exchange() 
        {
            // Get open id connect request.
            OpenIdConnectRequest request = HttpContext.GetOpenIdConnectRequest();

            if (request.IsPasswordGrantType()) 
            {
                var user = await _userManager.FindByNameAsync(request.Username);
                if (user == null) 
                {
                    return BadRequest(new OpenIdConnectResponse 
                    {
                        Error = OpenIdConnectConstants.Errors.InvalidGrant,
                        ErrorDescription = "The username/password couple is invalid."
                    });
                }

                // Ensure the user is allowed to sign in.
                if (!await _signInManager.CanSignInAsync(user)) 
                {
                    return BadRequest(new OpenIdConnectResponse 
                    {
                        Error = OpenIdConnectConstants.Errors.InvalidGrant,
                        ErrorDescription = "The specified user is not allowed to sign in."
                    });
                }

                // Reject the token request if two-factor authentication has been enabled by the user.
                if (_userManager.SupportsUserTwoFactor && await _userManager.GetTwoFactorEnabledAsync(user))
                {
                    return BadRequest(new OpenIdConnectResponse 
                    {
                        Error = OpenIdConnectConstants.Errors.InvalidGrant,
                        ErrorDescription = "The specified user is not allowed to sign in."
                    });
                }

                // Ensure the user is not already locked out.
                if (_userManager.SupportsUserLockout && await _userManager.IsLockedOutAsync(user)) 
                {
                    return BadRequest(new OpenIdConnectResponse 
                    {
                        Error = OpenIdConnectConstants.Errors.InvalidGrant,
                        ErrorDescription = "The username/password couple is invalid."
                    });
                }

                // Ensure the password is valid.
                if (!await _userManager.CheckPasswordAsync(user, request.Password)) 
                {
                    if (_userManager.SupportsUserLockout) 
                    {
                        await _userManager.AccessFailedAsync(user);
                    }

                    return BadRequest(new OpenIdConnectResponse 
                    {
                        Error = OpenIdConnectConstants.Errors.InvalidGrant,
                        ErrorDescription = "The username/password couple is invalid."
                    });
                }

                if (_userManager.SupportsUserLockout) 
                {
                    await _userManager.ResetAccessFailedCountAsync(user);
                }

                // Create a new authentication ticket.
                var ticket = await createTicketAsync(request, user);

                return SignIn(ticket.Principal, ticket.Properties, ticket.AuthenticationScheme);
            }
            else if (request.IsRefreshTokenGrantType()) 
            {
                // Retrieve the claims principal stored in the refresh token.
                var info = await HttpContext.Authentication.GetAuthenticateInfoAsync(OpenIdConnectServerDefaults.AuthenticationScheme);

                // Retrieve the user profile corresponding to the refresh token.
                var user = await _userManager.GetUserAsync(info.Principal);
                if (user == null) 
                {
                    return BadRequest(new OpenIdConnectResponse 
                    {
                        Error = OpenIdConnectConstants.Errors.InvalidGrant,
                        ErrorDescription = "The refresh token is no longer valid."
                    });
                }

                // Ensure the user is still allowed to sign in.
                if (!await _signInManager.CanSignInAsync(user)) 
                {
                    return BadRequest(new OpenIdConnectResponse 
                    {
                        Error = OpenIdConnectConstants.Errors.InvalidGrant,
                        ErrorDescription = "The user is no longer allowed to sign in."
                    });
                }

                // Create a new authentication ticket, but reuse the properties stored
                // in the refresh token, including the scopes originally granted.
                var ticket = await createTicketAsync(request, user, info.Properties);

                return SignIn(ticket.Principal, ticket.Properties, ticket.AuthenticationScheme);
            }

            return BadRequest(new OpenIdConnectResponse 
            {
                Error = OpenIdConnectConstants.Errors.UnsupportedGrantType,
                ErrorDescription = "The specified grant type is not supported."
            });
        }

        #endregion

        private async Task<AuthenticationTicket> createTicketAsync(OpenIdConnectRequest request, 
                                                                    ApplicationUser user,
                                                                    AuthenticationProperties properties = null) 
        {            
            // Create a new ClaimsPrincipal containing the claims that
            // will be used to create an id_token, a token or a code.
            var principal = await _signInManager.CreateUserPrincipalAsync(user);

            // Note: by default, claims are NOT automatically included in the access and identity tokens.
            // To allow OpenIddict to serialize them, you must attach them a destination, that specifies
            // whether they should be included in access tokens, in identity tokens or in both.

            foreach (var claim in principal.Claims) 
            {
                // In this sample, every claim is serialized in both the access and the identity tokens.
                // In a real world application, you'd probably want to exclude confidential claims
                // or apply a claims policy based on the scopes requested by the client application.
                claim.SetDestinations(OpenIdConnectConstants.Destinations.AccessToken,
                                      OpenIdConnectConstants.Destinations.IdentityToken);
            }

            // Create a new authentication ticket holding the user identity.
            var ticket = new AuthenticationTicket(principal, 
                                                    properties,
                                                    OpenIdConnectServerDefaults.AuthenticationScheme);

            if (!request.IsRefreshTokenGrantType()) 
            {
                // Set the list of scopes granted to the client application.
                // Note: the offline_access scope must be granted
                // to allow OpenIddict to return a refresh token.
                ticket.SetScopes(new[] 
                {
                    OpenIdConnectConstants.Scopes.OpenId,
                    OpenIdConnectConstants.Scopes.Email,
                    OpenIdConnectConstants.Scopes.Profile,
                    OpenIdConnectConstants.Scopes.OfflineAccess,
                    OpenIddictConstants.Scopes.Roles
                }.Intersect(request.GetScopes()));
            }
                
            // IMPORTANT TO INCLUDE RESOURCE SERVER URLS IN HERE.
            // Set valid resource servers for ticket.
            ticket.SetResources(_options.Resources);

            return ticket;
        }
    }
}