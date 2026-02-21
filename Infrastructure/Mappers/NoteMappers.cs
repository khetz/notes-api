using Application.Inputs;
using Domain.Entities;

namespace Infrastructure.Mappers
{
    internal static class NoteMappers
    {
        internal static Note ToNote(this CreateNoteRequest request, int userId) => new()
        {
            Title = request.Title,
            Content = request.Content,
            CategoryId = request.CategoryId,
            Category = null,
            UserId = userId,
            User = null,
            Order = 0
        };
    }
}
