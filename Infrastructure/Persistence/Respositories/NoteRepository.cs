using Application.Inputs;
using Application.Interfaces;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Respositories
{
    public class NoteRepository : INoteRepository
    {
        private readonly AppDbContext _appDbContext;

        public NoteRepository(AppDbContext appDbcontext) 
        {
            _appDbContext = appDbcontext; 
        }

        public async Task CreateAsync(Note note)
        {
            await _appDbContext.Notes.AddAsync(note);
            await _appDbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id, int userId)
        {
            var note = await _appDbContext
                .Notes.FirstOrDefaultAsync(n => n.Id == id && n.UserId == userId)
                ?? throw new KeyNotFoundException();

            _appDbContext.Notes.Remove(note);
            await _appDbContext.SaveChangesAsync();
        }

        public async Task<IReadOnlyCollection<Note>> GetAllAsync(int userId)
        {
            return await _appDbContext.Notes
                .Where(n => n.UserId == userId)
                .ToListAsync();
        }

        public async Task<Note> GetAsync(int id, int userId)
        {
            return await _appDbContext.Notes
                .FirstOrDefaultAsync(n => n.Id == id && n.UserId == userId)
                ?? throw new KeyNotFoundException();
        }

        public async Task MoveAsync(MoveNoteRequest request, int userId)
        {
            var note = await _appDbContext
                .Notes.FirstOrDefaultAsync(n => n.Id == request.Id && n.UserId == userId)
                ?? throw new KeyNotFoundException();

            note.Order = request.Order;
            note.CategoryId = request.CategoryId;

            await _appDbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(Note note)
        {
            var noteToUpdate = await _appDbContext.Notes
                .FirstOrDefaultAsync(n => n.Id == note.Id)
                ?? throw new KeyNotFoundException();

            noteToUpdate.Title = note.Title;
            noteToUpdate.Content = note.Content;

            await _appDbContext.SaveChangesAsync();
        }
    }
}
