using Application.Inputs;
using Application.Interfaces.Repositories;
using Application.Interfaces.Services;
using Application.Outputs;
using Application.Services;
using Domain.Entities;
using ErrorOr;
using Infrastructure.Mappers;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using System.Text.Json;

namespace Infrastructure.Services
{
    public class NoteService : INoteService
    {
        private readonly INoteRepository _noteRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly EmbeddingService _embeddingService;
        private readonly NoteAiService _noteAiService;

        public NoteService(INoteRepository noteRepository, IHttpContextAccessor httpContextAccessor, EmbeddingService embeddingService, NoteAiService noteAiService)
        {
            _noteRepository = noteRepository;
            _httpContextAccessor = httpContextAccessor;
            _embeddingService = embeddingService;
            _noteAiService = noteAiService;
        }

        public async Task CreatNoteAsync(CreateNoteRequest request)
        {
            var userId = GetUserId();

            var embedding = await EmbedNote(request.Title, request.Content);
            var note = request.ToNote(userId, embedding);
            await _noteRepository.CreateAsync(note);
        }

        public async Task<ErrorOr<Deleted>> DeleteNoteAsync(int id)
        {
            var userId = GetUserId();
            await _noteRepository.DeleteAsync(id, userId);
            return Result.Deleted;
        }

        public async Task<ErrorOr<IReadOnlyCollection<NoteResponse>>> GetAllNotesAsync()
        {
            var userId = GetUserId();
            var notes = await _noteRepository.GetAllAsync(userId);
            return notes.Select(n => n.ToNoteResponse()).ToList();
        }

        public async Task<ErrorOr<Note>> GetNoteAsync(int id)
        {
            var userId = GetUserId();
            var note = await _noteRepository.GetAsync(id, userId);
            return note;
        }

        public async Task<ErrorOr<Updated>> UpdateNoteAsync(UpdateNoteRequest request)
        {
            var userId = GetUserId();
            var updatedNote = request.ToNote(userId);
            await _noteRepository.UpdateAsync(updatedNote);

            return Result.Updated;
        }

        private int GetUserId()
        {
            var user = _httpContextAccessor.HttpContext?.User;
            if (user == null) throw new KeyNotFoundException(nameof(user));

            return int.Parse(user.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "");
        }

        private async Task<byte[]> EmbedNote(string noteTitle, string noteContent)
        {
            var vector = await _embeddingService.GetEmbeddingsAsync($"{noteTitle} {noteContent}");
            return EmbeddingService.ToBytes(vector);
        }

        public async Task<ErrorOr<IReadOnlyCollection<NoteResponse>>> PerformSemanticSearchAsync(string query)
        {
            var userId = GetUserId();
            var notes = await _noteRepository.PerformSemanticSearchAsync(query, userId);
            return notes.Select(n => n.ToNoteResponse()).ToList();
        }

        public async Task<ErrorOr<NoteAiResponse>> AnalyseNoteAsync(int id)
        {
            var note = await GetNoteAsync(id);

            if (note.IsError) return Error.NotFound();

            var noteValue = note.Value;
            var noteAnalysis = await _noteAiService.AnalyseNoteAsync(noteValue.Title, noteValue.Content, noteValue.Category?.Name ?? "");

            var updatedNote = new UpdateNoteRequest()
            {
                Id = noteValue.Id,
                Title = noteValue.Title,
                Content = noteValue.Content,
                Summary = noteAnalysis.Summary,
                Tags = JsonSerializer.Serialize(noteAnalysis.Tags)
            };

            await UpdateNoteAsync(updatedNote);

            return noteAnalysis;
        }
    }
}
