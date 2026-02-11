using Domain.Entities;

namespace Infrastructure.Models
{
    public class UserDbModel
    {
        public int Id { get; set; }
        public required string Username { get; set; }
        public required string PasswordHash { get; set; }

        public ICollection<Note> Notes { get; set; } = [];
    }
}
