namespace Domain.Entities
{
    public class Note
    {
        public int Id { get; set; }
        public required string Title { get; set; }
        public required string Content { get; set; }
        public required DateTime CreatedAt { get; set; }
        public required DateTime LastUpdatedAt { get; set; }
        public required int Order {  get; set; }

        public int CategoryId { get; set; }
        public required Category Category { get; set; }
    }
}
