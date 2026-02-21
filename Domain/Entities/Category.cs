namespace Domain.Entities
{
    public class Category
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime LastUpdatedAt { get; set; }

        public ICollection<Note> Notes { get; set; } = [];
        public int UserId { get; set; }
        public User? User { get; set; }
    }
}
