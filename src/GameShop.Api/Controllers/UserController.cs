using Auth0.ManagementApi;
using GameShop.Data.Repositories.Interfaces;
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
        IManagementApiClient _auth0ManagementApiClient;

        #endregion Declarations

        #region Constructors

        public UserController(IUserRepository userRepository, IManagementApiClient auth0ManagementApiClient)
        {
            _userRepository = userRepository;
            _auth0ManagementApiClient = auth0ManagementApiClient;
        }

        #endregion Constructors

        #region Methods

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var users = await _userRepository.GetAllUsersAsync();

            return Ok(users);
        }

        [HttpGet("id/{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var user = await _userRepository.FindUserById(id);

            if(user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        [HttpGet("username/{username}")]
        public async Task<IActionResult> GetByUsername(string username)
        {
            var user = await _userRepository.FindUserByUsername(username);

            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        [HttpGet("email/{email}")]
        public async Task<IActionResult> GetByEmail(string email)
        {
            var user = await _userRepository.FindUserByEmail(email);

            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        [HttpGet("claims")]
        public async Task<IActionResult> GetUserClaims([FromQuery]string code)
        {
            return Ok();
        }

        #endregion Methods
    }
}
