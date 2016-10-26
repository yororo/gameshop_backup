using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;

using GameShop.Api.Filters;
using GameShop.Contracts.Entities;
using GameShop.Data.Repositories.Interfaces;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace GameShop.Api.Controllers
{
    [Route("ads/games")]
    public class GameAdsController : Controller
    {
        private IGameAdvertisementAsyncRepository _gameAdRepository;

        public GameAdsController(IGameAdvertisementAsyncRepository gameAdRepository)
        {
            _gameAdRepository = gameAdRepository;
        }

        // GET: ads
        [HttpGet("getAll")]
        public async Task<IActionResult> GetAllAsync()
        {
            var ads = await _gameAdRepository.GetAllAsync();

            return Ok(ads);
        }
        
        // GET: ads/deep
        [HttpGet("getAllDeep")]
        public async Task<IActionResult> GetAllDeepAsync()
        {
            var ads = await _gameAdRepository.GetAllDeepAsync();

            return Ok(ads);
        }

        // GET ads/5
        [HttpGet("id/{id}")]
        public async Task<IActionResult> FindByIdAsync(Guid id)
        {
            var ad = await _gameAdRepository.FindByIdAsync(id);
            
            //Ad not found.
            if (ad == null)
            {
                return NotFound();
            }

            return Ok(ad);
        }

        // GET ads/5
        [HttpGet("fid/{friendlyId}")]
        public async Task<IActionResult> FindByFriendlyIdAsync(string friendlyId)
        {
            var ad = await _gameAdRepository.FindByFriendlyIdAsync(friendlyId);

            //Product not found.
            if (ad == null)
            {
                return NotFound();
            }

            return Ok(ad);
        }


        // GET ads/title
        [HttpGet("title/{title}")]
        public async Task<IActionResult> FindByTitleAsync(string title)
        {
            var ads = await _gameAdRepository.FindByTitleAsync(title);

            return Ok(ads);
        }

        // POST ads
        [HttpPost("create")]
        public async Task<IActionResult> CreateAsync([FromBody]Advertisement advertisement)
        {
            if (advertisement != null)
            {
                var result = await _gameAdRepository.AddAsync(advertisement);

                if (result > 0)
                {
                    return CreatedAtRoute(new { controller = nameof(GameAdsController), action = nameof(GameAdsController.FindByIdAsync), id = advertisement.AdvertisementId }, advertisement);
                }
            }

            return BadRequest();
        }

        // PUT ads/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(Guid id, [FromBody]Advertisement advertisement)
        {
            var result = await _gameAdRepository.UpdateAsync(id, advertisement);

            if (result > 0)
            {
                return Ok();
            }

            return BadRequest();
        }

        // DELETE ads/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            if (await _gameAdRepository.DeleteByIdAsync(id) != 0)
            {
                return Ok();
            }

            return BadRequest();
        }
    }
}
