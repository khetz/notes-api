using Application.Inputs;
using Application.Outputs;
using Domain.Entities;
using ErrorOr;

namespace Application.Interfaces.Services
{
    public interface INoteService
    {
        Task CreatNoteAsync(CreateNoteRequest request);
        Task<ErrorOr<Updated>> UpdateNoteAsync(UpdateNoteRequest request);
        Task<ErrorOr<Deleted>> DeleteNoteAsync(int id);
        Task<ErrorOr<Note>> GetNoteAsync(int id);
        Task<ErrorOr<IReadOnlyCollection<NoteResponse>>> GetAllNotesAsync();
        Task<ErrorOr<IReadOnlyCollection<NoteResponse>>> PerformSemanticSearchAsync(string query);
        Task AnalyseNoteAsync(int id);
    }
}
