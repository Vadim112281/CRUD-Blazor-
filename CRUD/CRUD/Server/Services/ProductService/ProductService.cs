using CRUD.Server.Data;
using CRUD.Shared;
using Microsoft.EntityFrameworkCore;

namespace CRUD.Server.Services.ProductService
{
    public class ProductService : IProductService
    {
        private readonly AppDbContext _context;

        public ProductService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Product> CreateProduct(Product product)
        {
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
            return product;
        }

        public async Task<bool> DeleteProduct(int productId)
        {
           var product = await _context.Products.FindAsync(productId); 
            if(product == null)
            {
                return false;
            }

            _context.Remove(product);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<Product?> GetProductById(int productId)
        {
            var product = await _context.Products.FindAsync(productId);

            return product;
        }

        public async Task<List<Product>> GetProducts()
        {
            return await _context.Products.ToListAsync();
        }

        public async Task<Product?> UpdateProduct(int productId, Product product)
        {
            var dbProduct = await _context.Products.FindAsync(productId);
            if(dbProduct != null)
            {
                dbProduct.Title = product.Title;
                dbProduct.Category = product.Category;
                dbProduct.Price = product.Price;

                await _context.SaveChangesAsync();
            }

            return dbProduct;
        }
    }
}
