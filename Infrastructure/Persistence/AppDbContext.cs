using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence
{
    public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
    {
        public DbSet<Note> Notes { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Note>()
                .HasOne(n => n.Category)
                .WithMany(c => c.Notes)
                .HasForeignKey(n => n.CategoryId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Note>()
                .HasOne(n => n.User)
                .WithMany(u => u.Notes)
                .HasForeignKey(n => n.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Category>()
                .HasOne(c => c.User)
                .WithMany()
                .HasForeignKey(c => c.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            SetTimestamps();
            return await base.SaveChangesAsync(cancellationToken);
        }

        private void SetTimestamps()
        {
            var notes = ChangeTracker.Entries<Note>()
                .Where(n => n.State == EntityState.Added || n.State == EntityState.Modified);

            foreach (var note in notes)
            {
                note.Entity.LastUpdatedAt = DateTime.UtcNow;

                if (note.State == EntityState.Added)
                {
                    note.Entity.CreatedAt = DateTime.UtcNow;
                }
            }

            var categories = ChangeTracker.Entries<Category>()
                .Where(c => c.State == EntityState.Added || c.State == EntityState.Modified);

            foreach (var category in categories)
            {
                category.Entity.LastUpdatedAt = DateTime.UtcNow;

                if (category.State == EntityState.Added)
                {
                    category.Entity.CreatedAt = DateTime.UtcNow;
                }
            }
        }
    }
}
