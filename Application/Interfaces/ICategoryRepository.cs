using Domain.Entities;

namespace Application.Interfaces
{
    public interface ICategoryRepository
    {
        Task CreateAsync(Category category);
        Task UpdateAsync(int categoryId, int userId, string name);
        Task<IReadOnlyCollection<Category>> GetAllAsync(int userId, bool includeNotes);
        Task DeleteAsync(int categoryId, int userId);
    }
}