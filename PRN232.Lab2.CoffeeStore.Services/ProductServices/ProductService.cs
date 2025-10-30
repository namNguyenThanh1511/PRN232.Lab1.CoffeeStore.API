using Microsoft.EntityFrameworkCore;
using PRN232.Lab2.CoffeeStore.Repositories.Entities;
using PRN232.Lab2.CoffeeStore.Repositories.UnitOfWork;
using PRN232.Lab2.CoffeeStore.Services.Exceptions;
using PRN232.Lab2.CoffeeStore.Services.Models;

namespace PRN232.Lab2.CoffeeStore.Services.ProductServices
{
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ProductService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // get all products
        public async Task<List<ProductResponse>> GetAllProductsAsync()
        {
            var products = await _unitOfWork.Products.GetAllAsync(
                q => q.Include(p => p.Category)
                      .Include(p => p.ProductInMenus)
                          .ThenInclude(pm => pm.Menu)
            );
            return MapToProductResponseList(products);
        }

        // get product by id
        public async Task<ProductDetailsResponse> GetProductByIdAsync(Guid id)
        {
            var product = await _unitOfWork.Products.GetByIdAsync(
                id,
                q => q.Include(p => p.Category)
                      .Include(p => p.ProductInMenus)
                          .ThenInclude(pm => pm.Menu)
            );

            if (product == null)
                throw new NotFoundException("Product not found");

            return MapToProductDetailsResponse(product);
        }

        // add product
        public async Task<ProductResponse> AddProductAsync(ProductCreationRequest request)
        {
            Product product = new Product
            {
                Name = request.Name,
                Description = request.Description,
                Price = request.Price,
                CategoryId = request.CategoryId
            };
            product = await _unitOfWork.Products.AddAsync(product);
            await _unitOfWork.SaveChangesAsync();
            var result = await _unitOfWork.Products.GetByIdAsync(product.Id, q => q.Include(p => p.Category));
            return MapToProductResponse(result);
        }

        // update product
        public async Task UpdateProductAsync(Guid id, ProductUpdationRequest request)
        {
            var existingProduct = await _unitOfWork.Products.GetByIdAsync(id);
            if (existingProduct == null)
                throw new NotFoundException("Product not found");

            existingProduct.Name = request.Name ?? existingProduct.Name;
            existingProduct.Description = request.Description ?? existingProduct.Description;
            existingProduct.Price = request.Price ?? existingProduct.Price;
            existingProduct.CategoryId = request.CategoryId ?? existingProduct.CategoryId;

            _unitOfWork.Products.Update(existingProduct);
            await _unitOfWork.SaveChangesAsync();
        }

        // delete product
        public async Task DeleteProductAsync(Guid id)
        {
            var existingProduct = await _unitOfWork.Products.GetByIdAsync(id);
            if (existingProduct == null)
                throw new NotFoundException("Product not found");

            _unitOfWork.Products.Remove(existingProduct);
            await _unitOfWork.SaveChangesAsync();
        }

        // mapping helpers
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

        private List<ProductResponse> MapToProductResponseList(IEnumerable<Product> products)
        {
            return products.Select(MapToProductResponse).ToList();
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
                Menus = product.ProductInMenus
                            .Where(pm => pm.Menu != null)
                            .Select(pm => new MenuResponse
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
