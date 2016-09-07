using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using GameShop.Data.Repositories.Interfaces;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace GameShop.Api.Controllers
{
    [Route("api/[controller]")]
    public class SearchController : Controller
    {
        private IAdRepository _adRepository;

        public SearchController(IAdRepository adRepository)
        {
            _adRepository = adRepository;
        }

        [HttpGet("ads/{id}")]
        public IActionResult SearchAdByFriendlyId(string id)
        {
            var products = _adRepository.FindByFriendlyId(id);

            if (products == null)
            {
                return NoContent();
            }

            return Ok(products);
        }

        [HttpGet("ads/title/{title}")]
        public IActionResult SearchAdByTitle(string title)
        {
            var products = _adRepository.FindByTitle(title);

            if (products == null)
            {
                return NoContent();
            }

            return Ok(products);
        }
    }
}
