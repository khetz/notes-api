namespace Application.Inputs
{
    public class UpdateNoteRequest
    {
        public int Id { get; set; }
        public required string Title { get; set; }
        public required string Content { get; set; }
        public string? Summary { get; set; }
        public string? Tags { get; set; }
    }
}
