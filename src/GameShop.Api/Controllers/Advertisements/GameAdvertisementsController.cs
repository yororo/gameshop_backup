using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;

using GameShop.Contracts.Entities;
using Microsoft.AspNetCore.Authorization;
using GameShop.Data.Contracts;
using GameShop.Api.Contracts;
using GameShop.Api.Contracts.Responses;
using GameShop.Api.Contracts.Constants;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace GameShop.Api.Controllers.Advertisements
{
    [Route(ApiEndpoints.GameAdvertisements)]
    public class GameAdvertisementsController : Controller
    {
        #region Declarations

        private readonly IGameAdvertisementRepository _gameAdvertisementsRepository;

        #endregion Declarations

        #region Constructors

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="gameAdvertisementsRepository">Game advertisements repository.</param>
        public GameAdvertisementsController(IGameAdvertisementRepository gameAdvertisementsRepository)
        {
            _gameAdvertisementsRepository = gameAdvertisementsRepository;
        }

        #endregion Constructors

        #region Game Advertisements

        /// <summary>
        /// Get all game advertisements.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery]string deep, [FromQuery]Guid? id, [FromQuery]string fid, [FromQuery]string title)
        {
            if(HttpContext.Request.Query.Count == 0)
            {
                // Get all game ads.
                return await GetAllAsync();
            }

            if(!string.IsNullOrEmpty(deep))
            {
                return await GetAllDeepAsync();
            }

            if(id.HasValue)
            {
                return await GetByIdAsync(id.Value);
            }
            
            if(!string.IsNullOrEmpty(fid))
            {
                return await GetByFriendlyIdAsync(fid);
            }

            if(!string.IsNullOrEmpty(title))
            {
                return await GetByTitleAsync(title);
            }

             return Ok(new ApiResponse(Result.Error, $"Unable to get advertisement/s."));
        }

        private async Task<IActionResult> GetAllAsync()
        {
            var ads = await _gameAdvertisementsRepository.GetAllAsync();

            return Ok(ads);
        }
        
        private async Task<IActionResult> GetAllDeepAsync()
        {
            var ads = await _gameAdvertisementsRepository.GetAllDeepAsync();

            return Ok(ads);
        }
        
        private async Task<IActionResult> GetByIdAsync(Guid id)
        {
            var ad = await _gameAdvertisementsRepository.FindByIdAsync(id);
            
            //Ad not found.
            if (ad == null)
            {
                return NotFound();
            }

            return Ok(ad);
        }
        
        private async Task<IActionResult> GetByFriendlyIdAsync(string friendlyId)
        {
            var advertisement = await _gameAdvertisementsRepository.FindByFriendlyIdAsync(friendlyId);

            //Ad not found.
            if (advertisement == null)
            {
                return NotFound();
            }

            return Ok(advertisement);
        }

        private async Task<IActionResult> GetByTitleAsync(string title)
        {
            var ads = await _gameAdvertisementsRepository.FindByTitleAsync(title);

            return Ok(ads);
        }
        
        //[Authorize]
        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody]Advertisement<Game> advertisement)
        {
            int result = await _gameAdvertisementsRepository.AddAsync(advertisement);

            if (result > 0)
            {
                return CreatedAtAction(nameof(GameAdvertisementsController.CreateAsync), advertisement);
            }

            return Ok(new ApiResponse(Result.Error, "Unable to create advertisement."));
        }

        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(Guid id, [FromBody] Advertisement<Game> advertisement)
        {
            if(await _gameAdvertisementsRepository.UpdateAsync(id, advertisement) > 0)
            {
                return Ok();
            }

            return Ok(new ApiResponse(Result.Error, $"Unable to update advertisement with id: { id }."));
        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            if (await _gameAdvertisementsRepository.DeleteByIdAsync(id) > 0)
            {
                return Ok();
            }

             return Ok(new ApiResponse(Result.Error, $"Unable to delete advertisement with id: { id }."));
        }

        #endregion Game Advertisements
    }
}
