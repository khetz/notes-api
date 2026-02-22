using Application.Inputs;
using ErrorOr;

namespace Application.Services
{
    public interface INoteService
    {
        Task CreatNoteAsync(CreateNoteRequest request);
        Task<ErrorOr<Updated>> UpdateNoteAsync(UpdateNoteRequest request);
        Task<ErrorOr<Deleted>> DeleteNoteAsync(int id);
    }
}
