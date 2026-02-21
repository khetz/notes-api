using Domain.Entities;

namespace Application.Interfaces
{
    public interface INoteRepository
    {
        Task CreateAsync(Note note);
    }
}
