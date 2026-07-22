namespace Application.Outputs
{
    public class NoteResponse
    {
        public int Id { get; set; }
        public required string Title { get; set; }
        public required string Content { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime LastUpdatedAt { get; set; }
        public int CategoryId { get; set; }
        public byte[]? Embedding { get; set; }
        public string? Summary { get; set; }
        public IReadOnlyCollection<string> Tags { get; set; } = [];
    }
}
