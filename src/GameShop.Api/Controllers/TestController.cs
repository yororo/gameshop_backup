using GameShop.Data.Repositories;
using GameShop.Data.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameShop.Api.Controllers
{
    [Route("[controller]")]
    public class TestController : Controller
    {
        IGameAdvertisementAsyncRepository _gameAdsRepository;

        public TestController(IGameAdvertisementAsyncRepository gameAdsRepository)
        {
            _gameAdsRepository = gameAdsRepository;
        }

        [HttpGet("guid")]
        public IActionResult GenerateGuid()
        {
            Guid id = Guid.NewGuid();

            return Ok(id);
        }

        [HttpGet("datetime")]
        public IActionResult GetCurrentDateTime()
        {
            return Ok(DateTime.Now);
        }

        [HttpGet("datetime/utc")]
        public IActionResult GetCurrentUtcDateTime()
        {
            return Ok(DateTime.UtcNow);
        }

        [HttpGet("test/1")]
        public IActionResult Test1()
        {
            return Ok(_gameAdsRepository.GetAllAsync());
        }
    }
}
