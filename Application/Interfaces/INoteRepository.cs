using Application.Inputs;
using Domain.Entities;

namespace Application.Interfaces
{
    public interface INoteRepository
    {
        Task CreateAsync(Note note);
        Task UpdateAsync(Note note);
        Task DeleteAsync(int id, int userId);
        Task MoveAsync(MoveNoteRequest request, int userId);
    }
}
