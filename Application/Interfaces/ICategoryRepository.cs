using Application.Outputs;
using Domain.Entities;

namespace Application.Interfaces
{
    public interface ICategoryRepository
    {
        Task<Category> CreateAsync(Category category);
        Task<Category> UpdateAsync(int categoryId, int userId, string name);
        Task<IReadOnlyCollection<Category>> GetAllAsync(int userId);
        Task DeleteAsync(int categoryId, int userId);
        Task<IReadOnlyCollection<Note>> GetNotesByCategoryIdAsync(int categoryId, int userId);
    }
}