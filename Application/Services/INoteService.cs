using Application.Inputs;
using Application.Outputs;
using Domain.Entities;
using ErrorOr;

namespace Application.Services
{
    public interface INoteService
    {
        Task CreatNoteAsync(CreateNoteRequest request);
        Task<ErrorOr<Updated>> UpdateNoteAsync(UpdateNoteRequest request);
        Task<ErrorOr<Deleted>> DeleteNoteAsync(int id);
        Task<ErrorOr<Note>> GetNoteAsync(int id);
        Task<ErrorOr<IReadOnlyCollection<NoteResponse>>> GetAllNotesAsync();
    }
}
