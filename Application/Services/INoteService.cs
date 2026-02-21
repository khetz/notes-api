using Application.Inputs;

namespace Application.Services
{
    public interface INoteService
    {
        Task CreatNoteAsync(CreateNoteRequest request);
    }
}
