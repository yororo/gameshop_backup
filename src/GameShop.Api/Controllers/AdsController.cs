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
        private IAdvertisementAsyncRepository _adRepository;

        public AdsController(IAdvertisementAsyncRepository adRepository)
        {
            _adRepository = adRepository;
        }

        // GET: api/ads
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var products = await _adRepository.GetAllAsync();

            if (products == null)
            {
                return NoContent();
            }

            return Ok(products);
        }

        // GET api/ads/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var ad = await _adRepository.FindByIdAsync(id);
            
            //Product not found.
            if (ad == null)
            {
                return NotFound();
            }

            return Ok(ad);
        }

        // POST api/ads
        [HttpPost]
        public async Task<IActionResult> Create([FromBody]Advertisement advertisement)
        {
            var result = await _adRepository.AddAsync(advertisement);

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
            var result = await _adRepository.UpdateAsync(id, advertisement);

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
            if (await _adRepository.DeleteByIdAsync(id) != 0)
            {
                return Ok();
            }

            return BadRequest();
        }
    }
}
