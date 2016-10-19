using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;

using GameShop.Api.Filters;
using GameShop.Contracts.Entities;
using GameShop.Data.Repositories;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace GameShop.Api.Controllers
{
    [Route("ads/games")]
    public class GameAdvertisementsController : Controller
    {
        private IGameAdvertisementRepository _advertisementRepository;

        public GameAdvertisementsController(IGameAdvertisementRepository advertisementRepository)
        {
            _advertisementRepository = advertisementRepository;
        }

        // GET: ads
        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var ads = await _advertisementRepository.GetAllAsync();

            return Ok(ads);
        }
        
        // GET: ads/deep
        [HttpGet("deep")]
        public async Task<IActionResult> GetAllDeepAsync()
        {
            var ads = await _advertisementRepository.GetAllDeepAsync();

            return Ok(ads);
        }

        // GET ads/5
        [HttpGet("id/{id}")]
        public async Task<IActionResult> FindByIdAsync(Guid id)
        {
            var ad = await _advertisementRepository.FindByIdAsync(id);
            
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
            var ad = await _advertisementRepository.FindByFriendlyIdAsync(friendlyId);

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
            var ads = await _advertisementRepository.FindByTitleAsync(title);

            return Ok(ads);
        }

        // POST ads
        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody]Advertisement<Game> advertisement)
        {
            var result = await _advertisementRepository.AddAsync(advertisement);

            if(result > 0)
            {
                return CreatedAtRoute(new { controller = nameof(GameAdvertisementsController), action = nameof(GameAdvertisementsController.FindByIdAsync), id = advertisement.AdvertisementId }, advertisement);
            }

            return BadRequest();
        }

        // PUT ads/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(Guid id, [FromBody]Advertisement<Game> advertisement)
        {
            var result = await _advertisementRepository.UpdateAsync(id, advertisement);

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
            if (await _advertisementRepository.DeleteByIdAsync(id) != 0)
            {
                return Ok();
            }

            return BadRequest();
        }
    }
}
