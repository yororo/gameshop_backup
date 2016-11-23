using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;

using GameShop.Contracts.Entities;
using Microsoft.AspNetCore.Authorization;
using GameShop.Data.Contracts;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace GameShop.Api.Controllers
{
    [Route("ads")]
    public class AdvertisementController : Controller
    {
        #region Declarations

        private IGameAdvertisementRepository _gameAdvertisementsRepository;

        #endregion Declarations

        #region Constructors

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="gameAdvertisementsRepository">Game advertisement repository.</param>
        public AdvertisementController(IGameAdvertisementRepository gameAdvertisementsRepository)
        {
            _gameAdvertisementsRepository = gameAdvertisementsRepository;
        }

        #endregion Constructors

        #region Game Advertisements

        /// <summary>
        /// Get all game advertisements.
        /// </summary>
        /// <returns></returns>
        [HttpGet("games/getAll")]
        public async Task<IActionResult> GetAllGameAdvertisementsAsync()
        {
            IEnumerable<Advertisement<Game>> ads = await _gameAdvertisementsRepository.GetAllAsync();

            return Ok(ads);
        }
        
        [HttpGet("games/getAllDeep")]
        public async Task<IActionResult> GetAllGameAdvertisementsDeepAsync()
        {
            IEnumerable<Advertisement<Game>> ads = await _gameAdvertisementsRepository.GetAllDeepAsync();

            return Ok(ads);
        }
        
        [HttpGet("games/id/{id}")]
        public async Task<IActionResult> FindGameAdvertisementByIdAsync(Guid id)
        {
            var ad = await _gameAdvertisementsRepository.FindByIdAsync(id);
            
            //Ad not found.
            if (ad == null)
            {
                return NotFound();
            }

            return Ok(ad);
        }
        
        [HttpGet("games/fid/{friendlyId}")]
        public async Task<IActionResult> FindGameAdvertisementByFriendlyIdAsync(string friendlyId)
        {
            var ad = await _gameAdvertisementsRepository.FindByFriendlyIdAsync(friendlyId);

            //Product not found.
            if (ad == null)
            {
                return NotFound(friendlyId);
            }

            return Ok(ad);
        }

        
        [HttpGet("games/title/{title}")]
        public async Task<IActionResult> FindGameAdvertisementByTitleAsync(string title)
        {
            var ads = await _gameAdvertisementsRepository.FindByTitleAsync(title);

            return Ok(ads);
        }
        
        [Authorize]
        [HttpPost("games")]
        public async Task<IActionResult> CreateGameAdvertisementAsync([FromBody]Advertisement<Game> advertisement)
        {
            if (advertisement != null)
            {
                var result = await _gameAdvertisementsRepository.AddAsync(advertisement);

                if (result > 0)
                {
                    return CreatedAtRoute(new { controller = nameof(AdvertisementController), action = nameof(AdvertisementController.FindGameAdvertisementByIdAsync), id = advertisement.AdvertisementId }, advertisement);
                }
            }

            return BadRequest(ModelState);
        }

        [Authorize]
        [HttpPut("games/{id}")]
        public async Task<IActionResult> UpdateGameAdvertisementAsync(Guid id, [FromBody]Advertisement<Game> advertisement)
        {
            var result = await _gameAdvertisementsRepository.UpdateAsync(id, advertisement);

            if (result > 0)
            {
                return Ok();
            }

            return BadRequest(ModelState);
        }

        [Authorize]
        [HttpDelete("games/{id}")]
        public async Task<IActionResult> DeleteGameAdvertisementAsync(Guid id)
        {
            if (await _gameAdvertisementsRepository.DeleteByIdAsync(id) != 0)
            {
                return Ok();
            }

            return BadRequest(ModelState);
        }

        #endregion Game Advertisements
    }
}
