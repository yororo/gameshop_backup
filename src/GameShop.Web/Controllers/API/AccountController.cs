using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using GameShop.Api.Contracts.Requests;
using GameShop.Contracts.Enumerations;
using Microsoft.AspNetCore.Identity;
using GameShop.Web.Models;
using GameShop.Api.Contracts;
using GameShop.Api.Contracts.Responses;

namespace GameShop.Web.Controllers.API
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class AccountController : Controller
    {
        private UserManager<ApplicationUser> _userManager;
        private SignInManager<ApplicationUser> _signManager;

        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signManager = signInManager;
        }

        // GET: api/Accounts
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(new string[] { "value1", "value2" });
        }

        // GET: api/Accounts/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            return Ok(id);
        }

        // POST: api/Accounts/Login
        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            //Instantiate a response.
            var response = new LoginResponse();
            response.Result = Result.Unknown;
            
            if (request == null || string.IsNullOrEmpty(request.Username) || string.IsNullOrEmpty(request.Password))
            {
                return BadRequest(response);
            }

            ApplicationUser user = await _userManager.FindByNameAsync(request.Username);

            // Ensure the password is valid.
            if (!await _userManager.CheckPasswordAsync(user, request.Password))
            {
                if (_userManager.SupportsUserLockout)
                {
                    await _userManager.AccessFailedAsync(user);
                }

                return BadRequest(response);
            }

            response.Result = Result.Success;     

            return Ok(response);
        }
    }
}
