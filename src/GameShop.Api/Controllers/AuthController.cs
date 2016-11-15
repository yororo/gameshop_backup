using GameShop.Api.Contracts;
using GameShop.Api.Contracts.Requests;
using GameShop.Api.Contracts.Responses;
using GameShop.Api.Services.Interfaces;
using GameShop.Contracts.Entities;
using GameShop.Data.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameShop.Api.Controllers
{
    [Route("auth")]
    public class AuthController : Controller
    {
        #region Declarations

        private IUserRepository _userRepository;
        private IPasswordHashingService _passwordHashingService;

        #endregion Declarations

        #region Constructors

        public AuthController(IUserRepository userRepository, IPasswordHashingService hashingService)
        {
            _userRepository = userRepository;
        }

        #endregion Constructors

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest loginRequest)
        {
            User user = await _userRepository.FindUserByUsername(loginRequest.Username);

            if(user == null)
            {
                return Unauthorized();
            }

            if(!_passwordHashingService.VerifyHashedPassword(user.Account.PasswordHash, loginRequest.Password))
            {
                return Unauthorized(); 
            }

            var response = new LoginResponse(Result.Success, "Login was successful.");
            response.UserProfile = user.Profile;

            return Ok(response);
        }

        [Authorize]
        [HttpGet("logout")]
        public async Task<IActionResult> Logout(string returnUrl)
        {
            return Redirect(returnUrl);
        }
        
        [HttpPost("signup")]
        public async Task<IActionResult> Signup([FromBody] SignupRequest signupRequest)
        {
            // Create a user.
            User user = new User();

            // User profile.
            user.Profile.ContactInformation.Add(new ContactInformation()
            {
                Email = signupRequest.Email
            });
            user.Profile.Name = new Name(signupRequest.Salutation, signupRequest.FirstName, signupRequest.LastName, signupRequest.LastName, signupRequest.Suffix);
            user.Profile.Gender = signupRequest.Gender;
            user.Profile.Birthday = signupRequest.Birthday;
            
            // User account.
            user.Account.Username = signupRequest.Username;
            user.Account.Email = signupRequest.Email;
            user.Account.PasswordHash = _passwordHashingService.HashPassword(signupRequest.Password);

            try
            {
                await _userRepository.AddUserAsync(user);

                return Created(HttpContext.Request.Path, new SignupResponse(Result.Success, "User successfully signed up."));
            }
            catch(Exception ex)
            {
                // Log error.

                return Json(new SignupResponse(Result.Error, "An error during signup occured."));
            }
        }

        [Authorize]
        [HttpGet("test")]
        public async Task<IActionResult> Test()
        {
            return new JsonResult(from c in User.Claims select new { c.Type, c.Value });
        }
    }
}
