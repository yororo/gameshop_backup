using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;

using GameShop.Api.Contracts;
using GameShop.Api.Contracts.Constants;
using GameShop.Api.Contracts.Responses;
using GameShop.Contracts.Serialization.Json;
using GameShop.Contracts.Entities;
using GameShop.Data.Contracts;
using Microsoft.Extensions.Logging;

namespace GameShop.Api.Controllers.Advertisements
{
    [Route(ApiEndpoints.Advertisements)]
    public class AdvertisementController : Controller
    {
        #region Declarations

        private readonly IAdvertisementRepository _advertisementsRepository;
        private ILogger<AdvertisementController> _logger;

        #endregion Declarations

        #region Constructors

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="advertisementsRepository">Game advertisements repository.</param>
        public AdvertisementController(IAdvertisementRepository advertisementsRepository, ILogger<AdvertisementController> logger)
        {
            _advertisementsRepository = advertisementsRepository;
            _logger = logger;
        }

        #endregion Constructors

        #region Advertisements

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

             return Error($"Unable to get advertisement/s.");
        }

        [HttpGet("{id}/products")]
        public async Task<IActionResult> GetAdvertisementProducts(Guid id)
        {
            var advertisements = await _advertisementsRepository.GetProductsAsync(id);

            return Ok(advertisements);
        }
        
        //[Authorize]
        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody]Advertisement advertisement)
        {
            int result = await _advertisementsRepository.AddAsync(advertisement);

            if (result > 0)
            {
                return CreatedAtAction(nameof(AdvertisementController.CreateAsync), advertisement);
            }

            return Error("Unable to create advertisement.");
        }

        //[Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(Guid id, [FromBody] Advertisement advertisement)
        {
            if(await _advertisementsRepository.UpdateAsync(id, advertisement) > 0)
            {
                return NoContent();
            }

            return Error($"Unable to update advertisement with id: { id }.");
        }

        //[Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            if (await _advertisementsRepository.DeleteByIdAsync(id) > 0)
            {
                return NoContent();
            }

            return Error($"Unable to delete advertisement with id: { id }.");
        }

        #endregion Advertisements
        
        #region Functions

        private async Task<IActionResult> GetAllAsync()
        {
            var advertisements = await _advertisementsRepository.GetAllAsync();

            return Ok(advertisements);
        }
        
        private async Task<IActionResult> GetAllDeepAsync()
        {
            var advertisements = await _advertisementsRepository.GetAllDeepAsync();

            return Ok(advertisements);
        }
        
        private async Task<IActionResult> GetByIdAsync(Guid id)
        {
            var advertisement = await _advertisementsRepository.GetByIdAsync(id);
            
            //Ad not found.
            if (advertisement == null)
            {
                return NotFound();
            }

            return Ok(advertisement);
        }
        
        private async Task<IActionResult> GetByFriendlyIdAsync(string friendlyId)
        {
            var advertisement = await _advertisementsRepository.GetByFriendlyIdAsync(friendlyId);

            //Ad not found.
            if (advertisement == null)
            {
                return NotFound();
            }

            return Ok(advertisement);
        }

        private async Task<IActionResult> GetByTitleAsync(string title)
        {
            var adverisements = await _advertisementsRepository.GetByTitleAsync(title);

            return Ok(adverisements);
        }

        /// <summary>
        /// Respond with OK result containing an error object.
        /// </summary>
        /// <param name="errorMessage"></param>
        /// <returns>OK result with error object.</returns>
        private IActionResult Error(string errorMessage)
        {
            return Ok(new ApiResponse(Result.Error, errorMessage));
        }

        #endregion Functions
    }
}
