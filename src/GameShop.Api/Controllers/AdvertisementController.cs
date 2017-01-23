﻿using System;
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
using Newtonsoft.Json;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace GameShop.Api.Controllers.Advertisements
{
    [Route(ApiEndpoints.Advertisements)]
    public class AdvertisementController : Controller
    {
        #region Declarations

        private readonly IAdvertisementRepository _advertisementsRepository;

        #endregion Declarations

        #region Constructors

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="advertisementsRepository">Game advertisements repository.</param>
        public AdvertisementController(IAdvertisementRepository advertisementsRepository)
        {
            _advertisementsRepository = advertisementsRepository;
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

             return Ok(new ApiResponse(Result.Error, $"Unable to get advertisement/s."));
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

            return Ok(new ApiResponse(Result.Error, "Unable to create advertisement."));
        }

        //[Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(Guid id, [FromBody] Advertisement advertisement)
        {
            if(await _advertisementsRepository.UpdateAsync(id, advertisement) > 0)
            {
                return Ok();
            }

            return Ok(new ApiResponse(Result.Error, $"Unable to update advertisement with id: { id }."));
        }

        //[Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            if (await _advertisementsRepository.DeleteByIdAsync(id) > 0)
            {
                return Ok();
            }

             return Ok(new ApiResponse(Result.Error, $"Unable to delete advertisement with id: { id }."));
        }

        private async Task<IActionResult> GetAllAsync()
        {
            var ads = await _advertisementsRepository.GetAllAsync();

            return Ok(ads);
        }
        
        private async Task<IActionResult> GetAllDeepAsync()
        {
            var ads = await _advertisementsRepository.GetAllDeepAsync();

            return Ok(ads);
        }
        
        private async Task<IActionResult> GetByIdAsync(Guid id)
        {
            var ad = await _advertisementsRepository.GetByIdAsync(id);
            
            //Ad not found.
            if (ad == null)
            {
                return NotFound();
            }

            return Ok(ad);
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
            var ads = await _advertisementsRepository.GetByTitleAsync(title);

            return Ok(ads);
        }

        #endregion Advertisements
    }
}
