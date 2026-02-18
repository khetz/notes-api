using Domain.Entities;
using Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence
{
    public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
    {
        public DbSet<NoteDbModel> Notes { get; set; }
        public DbSet<UserDbModel> Users { get; set; }
        public DbSet<CategoryDbModel> Categories { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }
    }
}
