using GameShop.Data.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameShop.Api.Controllers
{
    [Route("[users]")]
    public class UsersController : Controller
    {
        IUsersAsyncRepository _usersRepository;

        public UsersController(IUsersAsyncRepository usersRepository)
        {
            this._usersRepository = usersRepository;
        }

        public async Task<IActionResult> GetAllUsersAsync()
        {
            var users = await _usersRepository.GetAllUsersAsync();

            return Ok(users);
        }
    }
}
