using Application.Interfaces;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Respositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly AppDbContext _appDbContext;

        public CategoryRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task CreateAsync(Category category)
        {
            _appDbContext.Categories.Add(category);
            await _appDbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(int categoryId, int userId, string name)
        {
            var category = await _appDbContext.Categories
                .FirstOrDefaultAsync(c => c.Id == categoryId && c.UserId == userId);
            if (category == null) throw new KeyNotFoundException();

            category.Name = name;
            await _appDbContext.SaveChangesAsync();
        }
    }
}
