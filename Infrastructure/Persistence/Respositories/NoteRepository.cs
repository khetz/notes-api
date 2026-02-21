using Application.Interfaces;
using Domain.Entities;

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
    }
}
