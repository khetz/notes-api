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

        public async Task DeleteAsync(int categoryId, int userId)
        {
            var category = await _appDbContext.Categories
                .FirstOrDefaultAsync(c => c.Id == categoryId && c.UserId == userId)
                ?? throw new KeyNotFoundException();

            _appDbContext.Categories.Remove(category);
            await _appDbContext.SaveChangesAsync();
        }

        public async Task<IReadOnlyCollection<Category>> GetAllAsync(int userId)
        {
            var categoriesQuery = _appDbContext.Categories
                .Where(c => c.UserId == userId);

            return await categoriesQuery.ToListAsync();
        }

        public async Task<IReadOnlyCollection<Note>> GetNotesByCategoryIdAsync(int categoryId, int userId)
        {
            var notesQuery = _appDbContext.Notes
                .Where(n => n.UserId == userId && n.CategoryId == categoryId);

            return await notesQuery.ToListAsync();
        }

        public async Task UpdateAsync(int categoryId, int userId, string name)
        {
            var category = await _appDbContext.Categories
                .FirstOrDefaultAsync(c => c.Id == categoryId && c.UserId == userId)
                ?? throw new KeyNotFoundException();

            category.Name = name;
            await _appDbContext.SaveChangesAsync();
        }
    }
}
