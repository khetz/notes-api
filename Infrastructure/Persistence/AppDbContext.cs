using Domain.Entities;
using Infrastructure.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace Infrastructure.Persistence
{
    public class AppDbContext : DbContext
    {
        public DbSet<Note> Notes { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Category> Categories { get; set; }

        private readonly DatabaseConfiguration _databaseConfiguration;

        public AppDbContext(DbContextOptions<AppDbContext> options, IOptions<DatabaseConfiguration> databaseConfiguration)
            : base(options)
        {
            _databaseConfiguration = databaseConfiguration.Value;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
                optionsBuilder.UseSqlServer(_databaseConfiguration.ConnectionString);
        }
    }
}
