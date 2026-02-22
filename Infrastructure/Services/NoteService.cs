using Application.Inputs;
using Application.Interfaces;
using Application.Services;
using ErrorOr;
using Infrastructure.Mappers;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace Infrastructure.Services
{
    public class NoteService : INoteService
    {
        private readonly INoteRepository _noteRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public NoteService(INoteRepository noteRepository, IHttpContextAccessor httpContextAccessor)
        {
            _noteRepository = noteRepository;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task CreatNoteAsync(CreateNoteRequest request)
        {
            var userId = GetUserId();
            var note = request.ToNote(userId);
            await _noteRepository.CreateAsync(note);
        }

        public async Task<ErrorOr<Deleted>> DeleteNoteAsync(int id)
        {
            var userId = GetUserId();
            await _noteRepository.DeleteASync(id, userId);
            return Result.Deleted;
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
    }
}
