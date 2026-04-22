using Application.Inputs;
using Application.Outputs;
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
            User = null
        };

        internal static Note ToNote(this UpdateNoteRequest request, int userId) => new()
        {
            Id = request.Id,
            Title = request.Title,
            Content = request.Content,
            CategoryId = null,
            Category = null,
            UserId = userId,
            User = null
        };

        internal static NoteResponse ToNoteResponse(this Note note) => new()
        {
            Id = note.Id,
            Title = note.Title,
            Content = note.Content,
            LastUpdatedAt = note.LastUpdatedAt,
            CreatedAt = note.CreatedAt
        };
    }
}
