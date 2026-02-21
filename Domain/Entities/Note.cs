namespace Domain.Entities
{
    public class Note
    {
        public int Id { get; set; }
        public required string Title { get; set; }
        public required string Content { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime LastUpdatedAt { get; set; }
        public required int Order {  get; set; }

        public required int CategoryId { get; set; }
        public Category? Category { get; set; }
        public required int UserId { get; set; }
        public User? User { get; set; }
    }
}
