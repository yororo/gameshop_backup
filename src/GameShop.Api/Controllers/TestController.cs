using GameShop.Data.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http;
using Microsoft.Extensions.Logging;

namespace GameShop.Api.Controllers
{
    [Route("[controller]")]
    public class TestController : Controller
    {
        private ILogger _logger;
        
        public TestController(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger(typeof(TestController));
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

        [HttpGet("hash")]
        public IActionResult GenerateHash([FromQuery]string text)
        {
            return Ok(CryptoHelper.Crypto.HashPassword(text));
        }


        [Authorize]
        [HttpGet("admin")]
        public IActionResult Admin()
        {
            return Ok("Only logged in users can see this.");
        }

        [HttpGet("token")]
        public IActionResult Token()
        {
            using(var httpClient = new HttpClient())
            {
                return Ok();   
            }
        }
    }
}
