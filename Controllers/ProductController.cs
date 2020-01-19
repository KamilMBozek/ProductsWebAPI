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
        public IUnitOfWork UnitOfWork { get; private set; }

        public ProductController(IUnitOfWork unitOfWork)
        {
            this.UnitOfWork = unitOfWork;
        }

        // GET: api/Products
        [HttpGet]
        public IEnumerable<Product> GetList()
        {
            return UnitOfWork.Products.GetAll();
        }

        // GET: api/Products/5
        [HttpGet("{id}", Name = "Get")]
        [ProducesResponseType(typeof(Product), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetProductById(Guid id)
        {
            Product prod = UnitOfWork.Products.Get(id);
            if (prod == null)
            {
                return NotFound();
            }

            return Ok(prod);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult CreateProduct([FromBody] Product product)
        {
            if (product.Price == 0)
            {
                return BadRequest();
            }
            else
            {
                UnitOfWork.Products.Add(product);
                UnitOfWork.Complete();
            }

            return Ok(product.Id);
        }


        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult UpdateProduct(Guid id, [FromBody] Product product)
        {
            var entity = UnitOfWork.Products.Get(id);
            if (entity == null || product.Name.Length == 0 || product.Price == 0)
            {
                return BadRequest();
            }

            entity.Name = product.Name;
            entity.Price = product.Price;

            UnitOfWork.Products.Update(entity);
            UnitOfWork.Complete();

            return Ok();
        }


        [HttpDelete("{id}")]
        public void RemoveProduct(Guid id)
        {
            UnitOfWork.Products.RemoveById(id);
            UnitOfWork.Complete();
        }
    }
}
