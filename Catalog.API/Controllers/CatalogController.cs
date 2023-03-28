using Catalog.API.Entities;
using Catalog.API.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Catalog.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class CatalogController : ControllerBase
    {
        private readonly IReadProductRepository _readProductRepository;
        private readonly IWriteProductRepository _writeProductRepository;
        public CatalogController(IReadProductRepository readProductRepository,
                                 IWriteProductRepository writeProductRepository)
        {
            _readProductRepository = readProductRepository;
                        //?? throw new ArgumentNullException(nameof(readProductRepository));
            _writeProductRepository = writeProductRepository;
                        //?? throw new ArgumentNullException(nameof(writeProductRepository));
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Product>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts() 
        {
            var products = await _readProductRepository.GetProducts();
            return Ok(products);
        }

        [HttpGet("{id:length(24)}", Name = "GetProduct")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(Product), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<Product>>> GetProductsById(string id)
        {
            var product = await _readProductRepository.GetProduct(id);
            if(product is null)
            {
                return NotFound();
            }

            return Ok(product);
        }

        [Route("[action]/{category}", Name = "GetProductByCategory")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<Product>))]
        public async Task<ActionResult<IEnumerable<Product>>> GetProductsByCategory(string category)
        {
            if (category is null)
                return BadRequest("Invalid category");

            var products = await _readProductRepository.GetProductByCategory(category);
            
            return Ok(products);
        }

        [HttpPost]
        [ProducesResponseType(typeof(Product), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Product>> CreateProduct([FromBody] Product product)
        {
            if (product is null)
                return BadRequest("Invalid product");

            await _writeProductRepository.CreateProduct(product);

            return CreatedAtRoute("GetProduct", new { id = product.Id }, product);
        }

        [HttpPut]
        [ProducesResponseType(typeof(Product), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> UpdateProduct([FromBody] Product product)
        {
            if (product is null)
                return BadRequest("Invalid product");

            return Ok(await _writeProductRepository.UpdateProduct(product));
        }

        [HttpDelete]
        [ProducesResponseType(typeof(Product), StatusCodes.Status200OK)]
        public async Task<ActionResult> DeleteProductById(string id)
        {
            return Ok(await _writeProductRepository.DeleteProduct(id));
        }
    }
}
