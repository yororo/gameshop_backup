using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using GameShop.Data.Repositories.Interfaces;
using GameShop.Interface.Entities;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace GameShop.Core.Controllers
{
    [Route("api/[controller]")]
    public class ProductsController : Controller
    {
        private IProductRepository _productRepository;

        public ProductsController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        // GET: api/products
        [HttpGet]
        public IActionResult Get()
        {
            var products = _productRepository.GetAllProducts();

            if(products == null)
            {
                return NoContent();
            }

            return Ok(products);
        }

        // GET api/products/5
        [HttpGet("{id}")]
        public IActionResult Get(Guid id)
        {
            var product = _productRepository.GetProductById(id);

            //Product not found.
            if(product == null)
            {
                return NotFound();
            }

            return Ok(product);
        }

        [HttpGet("/search/{query}")]
        public IActionResult Search(string query)
        {
            var products = _productRepository.FindProductsByName(query);

            if(products == null)
            {
                return NoContent();
            }

            return Ok(products);
        }

        // POST api/products
        [HttpPost]
        public void Create([FromBody]PCGame product)
        {
        }

        // PUT api/products/5
        [HttpPut("{id}")]
        public void Update(int id, [FromBody]PCGame product)
        {
        }

        // DELETE api/products/5
        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            if (_productRepository.DeleteProduct(id))
            {
                return Ok();
            }

            return BadRequest();
        }
    }
}
