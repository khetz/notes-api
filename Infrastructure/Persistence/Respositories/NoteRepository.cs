using Application.Inputs;
using Application.Interfaces.Repositories;
using Application.Services;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Respositories
{
    public class NoteRepository : INoteRepository
    {
        private readonly AppDbContext _appDbContext;
        private readonly EmbeddingService _embeddingService;

        public NoteRepository(AppDbContext appDbcontext, EmbeddingService embeddingService) 
        {
            _appDbContext = appDbcontext;
            _embeddingService = embeddingService;
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
                .Include(n => n.Category)
                .FirstOrDefaultAsync(n => n.Id == id && n.UserId == userId)
                ?? throw new KeyNotFoundException();
        }

        public async Task<IReadOnlyCollection<Note>> PerformSemanticSearchAsync(string query, int userId)
        {
            var queryVector = await _embeddingService.GetEmbeddingsAsync(query);

            var notes = await _appDbContext.Notes
                .Where(n => n.UserId == userId && n.Embedding != null)
                .ToListAsync();

            var results = notes
                .Select(n => new
                {
                    Note = n,
                    Score = EmbeddingService.CosineSimilarity(
                        queryVector, EmbeddingService.FromBytes(n.Embedding!))
                })
                .OrderByDescending(x => x.Score)
                .Take(5)
                .Select(x => x.Note)
                .ToList();

            return results;
        }

        public async Task UpdateAsync(Note note)
        {
            var noteToUpdate = await _appDbContext.Notes
                .FirstOrDefaultAsync(n => n.Id == note.Id)
                ?? throw new KeyNotFoundException();

            noteToUpdate.Title = note.Title;
            noteToUpdate.Content = note.Content;
            noteToUpdate.Summary = note.Summary;
            noteToUpdate.Tags = note.Tags;

            await _appDbContext.SaveChangesAsync();
        }
    }
}
