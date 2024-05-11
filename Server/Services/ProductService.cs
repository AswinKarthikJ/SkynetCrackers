using Microsoft.EntityFrameworkCore;
using SkynetCrackers.Server.Data;
using SkynetCrackers.Shared;

namespace SkynetCrackers.Server.Services
{
    public interface IProductService
    {
        Task<List<Product>> GetAllProducts();
        Task<List<Product>> GetProductsByCategory(string categoryUrl);
        Task<Product> GetProduct(int id);
    }

    public class ProductService : IProductService
    {

        private readonly ICategoryService _categoryService;

        private readonly DataContext _context;

        public ProductService(ICategoryService categoryService, DataContext context)
        {
            _categoryService = categoryService;
            _context = context;
        }
        public async Task<List<Product>> GetAllProducts()
        {
            return await _context.Products.ToListAsync();
        }

        public async Task<Product> GetProduct(int id)
        {
            return await _context.Products.FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<List<Product>> GetProductsByCategory(string categoryUrl)
        {
            var selectedCategory = await _categoryService.GetCategoryByUrl(categoryUrl);

            return await _context.Products.Where(p => p.CategoryId == selectedCategory.Id).ToListAsync();
        }
    }
}
