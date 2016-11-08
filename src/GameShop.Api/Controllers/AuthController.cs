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
using System.Threading.Tasks;

namespace GameShop.Api.Controllers
{
    [Route("auth")]
    public class AuthController : Controller
    {
        #region Declarations

        #endregion Declarations

        #region Constructors

        #endregion Constructors

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest loginRequest)
        {
            return null;
        }

        [Authorize]
        [HttpGet("logout")]
        public async Task<IActionResult> Logout(string returnUrl)
        {
            return null;
        }

        [Authorize]
        [HttpGet("test")]
        public async Task<IActionResult> Test()
        {
            return new JsonResult(from c in User.Claims select new { c.Type, c.Value });
        }
    }
}
