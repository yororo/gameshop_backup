using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using GameShop.Data.Repositories.Interfaces;
using GameShop.Contracts.Entities;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace GameShop.Api.Controllers
{
    [Route("api/[controller]")]
    public class AdsController : Controller
    {
        private IAdvertisementAsyncRepository _advertisementRepository;

        public AdsController(IAdvertisementAsyncRepository advertisementRepository)
        {
            _advertisementRepository = advertisementRepository;
        }

        // GET: api/ads
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var ads = await _advertisementRepository.GetAllAsync();

            return Ok(ads);
        }

        // GET api/ads/5
        [HttpGet("id/{id}")]
        public async Task<IActionResult> FindById(Guid id)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var ad = await _advertisementRepository.FindByIdAsync(id);
            
            //Ad not found.
            if (ad == null)
            {
                return NotFound();
            }

            return Ok(ad);
        }

        // GET api/ads/5
        [HttpGet("fid/{friendlyId}")]
        public async Task<IActionResult> FindByFriendlyId(int friendlyId)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var ad = await _advertisementRepository.FindByFriendlyIdAsync(friendlyId.ToString());

            //Product not found.
            if (ad == null)
            {
                return NotFound();
            }

            return Ok(ad);
        }


        // GET api/ads/title
        [HttpGet("title/{title}")]
        public async Task<IActionResult> FindByTitle(string title)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var ads = await _advertisementRepository.FindByTitleAsync(title);

            return Ok(ads);
        }

        // POST api/ads
        [HttpPost]
        public async Task<IActionResult> Create([FromBody]Advertisement advertisement)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _advertisementRepository.AddAsync(advertisement);

            if(result > 0)
            {
                return CreatedAtRoute(new { controller = "Ads", action = "GetById", id = advertisement.Id }, advertisement);
            }

            return BadRequest();
        }

        // PUT api/ads/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody]Advertisement advertisement)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _advertisementRepository.UpdateAsync(id, advertisement);

            if (result > 0)
            {
                return Ok();
            }

            return BadRequest();
        }

        // DELETE api/ads/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (await _advertisementRepository.DeleteByIdAsync(id) != 0)
            {
                return Ok();
            }

            return BadRequest();
        }
    }
}
