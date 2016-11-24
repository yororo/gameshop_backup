using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using GameShop.Data.Contracts;
using GameShop.Contracts.Entities;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace GameShop.Api.Controllers
{
    [Route("product")]
    public class ProductController : Controller
    {
        #region Declarations

        private readonly IProductRepository _productRepository;

        #endregion Declarations

        #region Properties
        /// <summary>
        /// Product repository instance that has access to database.
        /// </summary>
        public IProductRepository ProductRepository
        {
            get
            {
                return _productRepository;
            }
        }

        #endregion Properties

        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="productRepository"></param>
        public ProductController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        #endregion Constructors

        #region Methods
        public async Task<IActionResult> GetAllAsync()
        {
            IEnumerable<Product> results = await ProductRepository.GetAllAsync();

            if (results == null)
            {
                return NotFound();
            }

            return Ok(results);
        }

        public async Task<IActionResult> GetByIdAsync(Guid id)
        {
            Product result = await ProductRepository.GetByIdAsync(id);

            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        public async Task<IActionResult> GetByNameAsync(string name)
        {
            IEnumerable<Product> results = await ProductRepository.GetByNameAsync(name);

            if (results == null)
            {
                return NotFound();
            }

            return Ok(results);
        }
        #endregion Methods
    }
}
