using SkynetCrackers.Shared;

namespace SkynetCrackers.Server.Services
{
    public interface ICategoryService
    {
        Task<List<Category>> GetCategories();

        Category GetCategoryByUrl(string categoryUrl);
    }
    public class CategoryService : ICategoryService
    {
        public List<Category> categories { get; set; } = new List<Category> {
                new Category { Id = 1, Name = "Books", Url = "books", Icon = "book" },
                new Category { Id = 2, Name = "Electronics", Url = "electronics", Icon = "camera-slr" },
                new Category { Id = 3, Name = "Video Games", Url = "video-games", Icon = "aperture" }
            };

        public async Task<List<Category>> GetCategories()
        {
            return categories;
        }

        public Category GetCategoryByUrl(string categoryUrl)
        {
            return categories.
                FirstOrDefault(c => c.Url.ToLower().Equals(categoryUrl.ToLower()));
        }
    }
}
