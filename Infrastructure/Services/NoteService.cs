using Application.Inputs;
using Application.Interfaces;
using Application.Services;
using Domain.Entities;
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
            await _noteRepository.DeleteAsync(id, userId);
            return Result.Deleted;
        }

        public async Task<ErrorOr<Note>> GetNoteAsync(int id)
        {
            var userId = GetUserId();
            var note = await _noteRepository.GetAsync(id, userId);
            return note;
        }

        public async Task<ErrorOr<Updated>> MoveNoteAsync(MoveNoteRequest request)
        {
            var userId = GetUserId();
            await _noteRepository.MoveAsync(request, userId);
            return Result.Updated;
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
