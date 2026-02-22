using Application.Inputs;
using Domain.Entities;
using ErrorOr;

namespace Application.Services
{
    public interface INoteService
    {
        Task CreatNoteAsync(CreateNoteRequest request);
        Task<ErrorOr<Updated>> UpdateNoteAsync(UpdateNoteRequest request);
        Task<ErrorOr<Deleted>> DeleteNoteAsync(int id);
        Task<ErrorOr<Updated>> MoveNoteAsync(MoveNoteRequest request);
        Task<ErrorOr<Note>> GetNoteAsync(int id);
    }
}
