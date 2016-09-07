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
        private IAdAsyncRepository _adRepository;

        public AdsController(IAdAsyncRepository productRepository)
        {
            _adRepository = productRepository;
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
            var product = await _adRepository.FindByIdAsync(id);
            
            //Product not found.
            if (product == null)
            {
                return NotFound();
            }

            return Ok(product);
        }

        // POST api/ads
        [HttpPost]
        public void Create([FromBody]Ad product)
        {
        }

        // PUT api/ads/5
        [HttpPut("{id}")]
        public void Update(int id, [FromBody]Ad product)
        {
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
