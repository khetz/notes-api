using Domain.Entities;
using Infrastructure.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace Infrastructure.Persistence
{
    public class AppDbContext :  DbContext
    {
        public required DbSet<Note> Notes { get; set; }
        public required DbSet<User> Users { get; set; }
        public required DbSet<Category> Categories { get; set; }

        private readonly DatabaseConfiguration _databaseConfiguration;

        public AppDbContext(IOptions<DatabaseConfiguration> databaseConfiguration)
        {
            _databaseConfiguration = databaseConfiguration.Value;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_databaseConfiguration.ConnectionString);
        }
    }
}
