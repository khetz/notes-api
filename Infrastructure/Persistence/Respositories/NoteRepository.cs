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
