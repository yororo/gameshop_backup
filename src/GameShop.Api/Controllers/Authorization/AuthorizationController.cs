// using System.Linq;
// using System.Threading.Tasks;
// using AspNet.Security.OpenIdConnect.Extensions;
// using AspNet.Security.OpenIdConnect.Primitives;
// using AspNet.Security.OpenIdConnect.Server;
// using Microsoft.AspNetCore.Authentication;
// using Microsoft.AspNetCore.Http.Authentication;
// using Microsoft.AspNetCore.Identity;
// using Microsoft.AspNetCore.Mvc;
// using OpenIddict;
// using GameShop.Api.Services.Interfaces;
// using GameShop.Data.Contracts;
// using GameShop.Contracts.Entities;
// using Microsoft.AspNetCore.Authorization;

// namespace GameShop.Api.Controllers
// {
//     [Route("authorization")]
//     public class AuthorizationController : Controller
//     {
//         private readonly SignInManager<Authorization.Models.User> _signInManager;
//         private readonly UserManager<Authorization.Models.User> _userManager;

//         public AuthorizationController(SignInManager<Authorization.Models.User> signInManager,
//                                         UserManager<Authorization.Models.User> userManager)
//         {
//             _signInManager = signInManager;
//             _userManager = userManager;   
//         }

//         [HttpPost("~/connect/token"), Produces("application/json")]
//         public async Task<IActionResult> Exchange(OpenIdConnectRequest request)
//         {
//             // grant_type: password
//             if (request.IsPasswordGrantType())
//             {
//                 Authorization.Models.User user = await _userManager.FindByNameAsync(request.Username);
//                 if (user == null)
//                 {
//                     return BadRequest(new OpenIdConnectResponse
//                     {
//                         Error = OpenIdConnectConstants.Errors.InvalidGrant,
//                         ErrorDescription = "The username/password couple is invalid."
//                     });
//                 }

//                 // Ensure the user is allowed to sign in.
//                 if (!await _signInManager.CanSignInAsync(user))
//                 {
//                     return BadRequest(new OpenIdConnectResponse
//                     {
//                         Error = OpenIdConnectConstants.Errors.InvalidGrant,
//                         ErrorDescription = "The specified user is not allowed to sign in."
//                     });
//                 }

//                 // Reject the token request if two-factor authentication has been enabled by the user.
//                 if (await _userManager.GetTwoFactorEnabledAsync(user))
//                 {
//                     return BadRequest(new OpenIdConnectResponse
//                     {
//                         Error = OpenIdConnectConstants.Errors.InvalidGrant,
//                         ErrorDescription = "The specified user is not allowed to sign in."
//                     });
//                 }

//                 // Ensure the user is not already locked out.
//                 if (await _userManager.IsLockedOutAsync(user))
//                 {
//                     return BadRequest(new OpenIdConnectResponse
//                     {
//                         Error = OpenIdConnectConstants.Errors.InvalidGrant,
//                         ErrorDescription = "The username/password couple is invalid."
//                     });
//                 }

//                 // Ensure the password is valid.
//                 if (!await _userManager.CheckPasswordAsync(user, request.Password))
//                 {
//                     return BadRequest(new OpenIdConnectResponse
//                     {
//                         Error = OpenIdConnectConstants.Errors.InvalidGrant,
//                         ErrorDescription = "The username/password couple is invalid."
//                     });
//                 }

//                 // Create a new authentication ticket.
//                 var ticket = await createTicketAsync(request, user);

//                 return SignIn(ticket.Principal, ticket.Properties, ticket.AuthenticationScheme);
//             }

//             return BadRequest(new OpenIdConnectResponse
//             {
//                 Error = OpenIdConnectConstants.Errors.UnsupportedGrantType,
//                 ErrorDescription = "The specified grant type is not supported."
//             });
//         }

//         /// <summary>
//         /// Create authentication ticket.
//         /// </summary>
//         /// <param name="request">OpenIdConnectRequest.</param>
//         /// <param name="user">User.</param>
//         /// <returns></returns>
//         private async Task<AuthenticationTicket> createTicketAsync(OpenIdConnectRequest request, Authorization.Models.User user)
//         {
//             // Create a new ClaimsPrincipal containing the claims that
//             // will be used to create an id_token, a token or a code.
//             var principal = await _signInManager.CreateUserPrincipalAsync(user);

//             // Note: by default, claims are NOT automatically included in the access and identity tokens.
//             // To allow OpenIddict to serialize them, you must attach them a destination, that specifies
//             // whether they should be included in access tokens, in identity tokens or in both.

//             foreach (var claim in principal.Claims)
//             {
//                 // In this sample, every claim is serialized in both the access and the identity tokens.
//                 // In a real world application, you'd probably want to exclude confidential claims
//                 // or apply a claims policy based on the scopes requested by the client application.
//                 claim.SetDestinations(OpenIdConnectConstants.Destinations.AccessToken,
//                                       OpenIdConnectConstants.Destinations.IdentityToken);
//             }

//             // Create a new authentication ticket holding the user identity.
//             var ticket = new AuthenticationTicket(principal, 
//                                                 new AuthenticationProperties(),
//                                                 OpenIdConnectServerDefaults.AuthenticationScheme);

//             // Set the list of scopes granted to the client application.
//             // Note: the offline_access scope must be granted
//             // to allow OpenIddict to return a refresh token.
//             ticket.SetScopes(new[] {
//                 OpenIdConnectConstants.Scopes.OpenId,
//                 OpenIdConnectConstants.Scopes.Email,
//                 OpenIdConnectConstants.Scopes.Profile,
//                 OpenIdConnectConstants.Scopes.OfflineAccess,
//                 OpenIddictConstants.Scopes.Roles
//             }.Intersect(request.GetScopes()));

//             return ticket;
//         }
//     }
// }