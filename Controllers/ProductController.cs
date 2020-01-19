using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProductsWebAPI.Data;

namespace ProductsWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private IUnitOfWork unitOfWork;

        public ProductController(IUnitOfWork unit)
        {
            unitOfWork = unit;
        }

        // GET: api/Product
        [HttpGet]
        public async Task<IEnumerable<Product>> GetList()
        {
            return await unitOfWork.Products.GetAll();
        }

        // GET: api/Product/id
        [HttpGet("{id}", Name = "GetProductById")]
        [ProducesResponseType(typeof(Product), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetProductById(Guid id)
        {
            Product prod = await unitOfWork.Products.Get(id);
            if (prod == null)
            {
                return NotFound();
            }

            return Ok(prod);
        }

        // POST: api/Product
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateProduct([FromBody] Product product)
        {
            if (product.Price == 0)
            {
                return BadRequest();
            }
            else
            {
                unitOfWork.Products.Add(product);
                await unitOfWork.CompleteAsync();
            }

            return Ok(product.Id);
        }

        // PUT: api/Product/id
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateProduct(Guid id, [FromBody] Product product)
        {
            var entity = await unitOfWork.Products.Get(id);
            if (entity == null || product.Name.Length == 0 || product.Price == 0)
            {
                return BadRequest();
            }

            entity.Name = product.Name;
            entity.Price = product.Price;

            unitOfWork.Products.Update(entity);
            unitOfWork.Complete();

            return Ok();
        }

        // DELETE: api/Product/id
        [HttpDelete("{id}")]
        public async Task RemoveProduct(Guid id)
        {
            await unitOfWork.Products.RemoveById(id);
            await unitOfWork.CompleteAsync();
        }
    }
}
