using PRN232.Lab1.CoffeeStore.Data.Entities;
using PRN232.Lab1.CoffeeStore.Data.Repositories;
using PRN232.Lab1.CoffeeStore.Service.Models;

namespace PRN232.Lab1.CoffeeStore.Service
{
    public class ProductService
    {
        private readonly ProductRepository _productRepository;
        public ProductService(ProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        //get all products
        public async Task<IEnumerable<ProductResponse>> GetAllProductsAsync()
        {
            var products = await _productRepository.GetAllProductsAsync();
            //map to ProductResponse
            return MapToProductResponseList(products);
        }
        //get product by id
        public async Task<ProductResponse> GetProductByIdAsync(Guid id)
        {
            var product = await _productRepository.GetProductByIdAsync(id);
            if (product == null)
            {
                throw new KeyNotFoundException("Product not found");
            }
            return MapToProductDetailsResponse(product);
        }
        //add product
        public async Task<ProductResponse> AddProductAsync(ProductCreationRequest request)
        {
            Product product = new Product
            {
                Name = request.Name,
                Description = request.Description,
                Price = request.Price,
                CategoryId = request.CategoryId
            };
            product = await _productRepository.AddProductAsync(product);
            return MapToProductResponse(product);
        }
        //update product
        public async Task UpdateProductAsync(Guid id, ProductUpdationRequest request)
        {
            var existingProduct = await _productRepository.GetProductByIdAsync(id);
            if (existingProduct == null)
            {
                throw new Exception("Product not found");
            }
            existingProduct.Name = request.Name ?? existingProduct.Name;
            existingProduct.Description = request.Description ?? existingProduct.Description;
            existingProduct.Price = request.Price ?? existingProduct.Price;
            existingProduct.CategoryId = request.CategoryId ?? existingProduct.CategoryId;
            await _productRepository.UpdateProductAsync(existingProduct);
        }
        //delete product
        public async Task DeleteProductAsync(Guid id)
        {
            var existingProduct = await _productRepository.GetProductByIdAsync(id);
            if (existingProduct == null)
            {
                throw new Exception("Product not found");
            }
            await _productRepository.DeleteProductAsync(id);
        }

        private ProductResponse MapToProductResponse(Product product)
        {
            return new ProductResponse
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                Category = product.Category?.Name
            };
        }

        private IEnumerable<ProductResponse> MapToProductResponseList(IEnumerable<Product> products)
        {
            return products.Select(p => MapToProductResponse(p));
        }

        private ProductDetailsResponse MapToProductDetailsResponse(Product product)
        {
            return new ProductDetailsResponse
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                Category = product.Category?.Name,
                CategoryId = product.CategoryId,
                Menus = product.ProductInMenus.Where(pm => pm.Menu != null).Select(pm => new MenuResponse
                {
                    Id = pm.Menu.Id,
                    Name = pm.Menu.Name,
                    FromDate = pm.Menu.FromDate,
                    ToDate = pm.Menu.ToDate
                }).ToList()

            };
        }



    }
}
