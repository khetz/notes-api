using Application.Inputs;
using Application.Interfaces;
using Application.Services;
using Infrastructure.Mappers;
using Microsoft.AspNetCore.Http;

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
            var user = _httpContextAccessor.HttpContext?.User;
            var userId = int.Parse(user?.Identity?.Name ?? "");

            var note = request.ToNote(userId);
            await _noteRepository.CreateAsync(note);
        }
    }
}
