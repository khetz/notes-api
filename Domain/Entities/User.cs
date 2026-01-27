namespace Domain.Entities
{
    public class User
    {
        public int Id { get; set; }
        public required string Username { get; set; }
        public required string PasswordHash { get; set; }

        public ICollection<Note> Notes { get; set; } = [];
    }
}
