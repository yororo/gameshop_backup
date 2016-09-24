using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using GameShop.Data.Repositories.Interfaces;
using GameShop.Contracts.API.Requests;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace GameShop.Api.Controllers
{
    [Route("[controller]")]
    public class SearchController : Controller
    {
        private IGameAdvertisementAsyncRepository _adRepository;

        public SearchController(IGameAdvertisementAsyncRepository adRepository)
        {
            _adRepository = adRepository;
        }

        [HttpPost("ads")]
        public IActionResult SearchGameAds([FromBody]SearchGameRequest searchAdRequest)
        {
            return Ok();
        }

        [HttpGet("ads/games/id/{id}")]
        public IActionResult SearchAdById(Guid id)
        {
            return RedirectToRoute(new { controller = nameof(GameAdsController).Replace(nameof(Controller), string.Empty),
                                            action = nameof(GameAdsController.FindByIdAsync),
                                            @id = id });
        }

        [HttpGet("ads/games/fid/{friendlyId}")]
        public IActionResult SearchAdByFriendlyId(string friendlyId)
        {
            return RedirectToRoute(new { controller = nameof(GameAdsController).Replace(nameof(Controller), string.Empty),
                                            action = nameof(GameAdsController.FindByFriendlyIdAsync),
                                            @friendlyId = friendlyId });
        }

        [HttpGet("ads/games/title/{title}")]
        public IActionResult SearchAdByTitle(string title)
        {
            return RedirectToRoute(new { controller = nameof(GameAdsController).Replace(nameof(Controller), string.Empty),
                                            action = nameof(GameAdsController.FindByTitleAsync),
                                            @title = title });
        }
    }
}
