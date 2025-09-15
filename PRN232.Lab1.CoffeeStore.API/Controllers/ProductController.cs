using Microsoft.AspNetCore.Mvc;
using PRN232.Lab1.CoffeeStore.Data.Entities;

namespace PRN232.Lab1.CoffeeStore.API.Controllers
{
    [Route("api/products")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        // Implement CRUD operations for Product entity here

        private readonly Data.Repositories.ProductRepository _productRepository;
        public ProductController(Data.Repositories.ProductRepository productRepository)
        {
            _productRepository = productRepository;
        }
        //api get all 
        [HttpGet]
        public IActionResult GetAllProducts()
        {
            var products = _productRepository.GetAllProducts();
            return Ok(products);
        }
        //api get by id
        [HttpGet("{id}")]
        public IActionResult GetProduct(Guid id)
        {
            var product = _productRepository.GetProductById(id);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }
        //api create
        [HttpPost]
        public IActionResult CreateProduct([FromBody] Data.Entities.Product product)
        {
            if (product == null)
            {
                return BadRequest();
            }
            _productRepository.AddProduct(product);
            return CreatedAtAction(nameof(GetProduct), new { id = product.Id }, product);
        }
        //api update product details
        [HttpPut("{id}")]
        public IActionResult UpdateProduct(Guid id, Product product)
        {
            var existingProduct = _productRepository.GetProductById(id);

            if (existingProduct == null)
            {
                return NotFound();
            }
            existingProduct.Name = product.Name;
            existingProduct.Description = product.Description;
            existingProduct.Price = product.Price;
            existingProduct.CategoryId = product.CategoryId;

            _productRepository.UpdateProduct(existingProduct);
            return NoContent();
        }

        //api delete product
        [HttpDelete("{id}")]
        public IActionResult DeleteProduct(Guid id)
        {
            var existingProduct = _productRepository.GetProductById(id);
            if (existingProduct == null)
            {
                return NotFound();
            }
            _productRepository.DeleteProduct(id);
            return NoContent();
        }
    }
}
