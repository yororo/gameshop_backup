using Auth0.ManagementApi;
using GameShop.Api.Contracts;
using GameShop.Api.Contracts.Responses;
using GameShop.Contracts.Entities;
using GameShop.Data.Contracts;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameShop.Api.Controllers
{
    [Route("users")]
    public class UserController : Controller
    {
        #region Declarations

        private IUserRepository _userRepository;

        #endregion Declarations

        #region Constructors

        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        #endregion Constructors

        #region Methods

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery]Guid? id = null, [FromQuery]string username = null, [FromQuery]string email = null)
        {
            // No query parameters. Get all users.
            if (HttpContext.Request.Query.Count == 0)
            {
                return Ok(await _userRepository.GetAllUsersAsync());
            }

            User user = null;

            if(id.HasValue)
            {
                user = await _userRepository.FindUserById(id.Value);
            }
            else if(!string.IsNullOrEmpty(username))
            {
                user = await _userRepository.FindUserByUsername(username);
            }
            else if (!string.IsNullOrEmpty(email))
            {
                user = await _userRepository.FindUserByEmail(email);
            }

            // Check if a user is found.
            if (user != null)
            {
                return Ok(user);
            }

            return NotFound();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            User user = await _userRepository.FindUserById(id);

            if(user == null)
            {
                return NotFound();
            }

            int result = await _userRepository.DeleteUserByIdAsync(id);

            if (result == 0)
            {
                return Ok(new ApiResponse(Result.Failure, $"Unable to delete user with id:{ id }"));
            }

            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody]User user)
        {
            int result = await _userRepository.AddUserAsync(user);

            if (result == 0)
            {
                return Ok(new ApiResponse(Result.Failure, $"Unable to create user."));
            }

            return Ok(user);
        }

        #endregion Methods
    }
}
