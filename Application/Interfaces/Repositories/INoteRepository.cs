using Application.Inputs;
using Domain.Entities;

namespace Application.Interfaces.Repositories
{
    public interface INoteRepository
    {
        Task CreateAsync(Note note);
        Task UpdateAsync(Note note);
        Task DeleteAsync(int id, int userId);
        Task<Note> GetAsync(int id, int userId);
        Task<IReadOnlyCollection<Note>> GetAllAsync(int userId);
    }
}
