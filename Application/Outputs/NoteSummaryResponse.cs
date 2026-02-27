namespace Application.Outputs
{
    public class NoteSummaryResponse
    {
        public int Id { get; set; }
        public required string Title { get; set; }
        public required string Content { get; set; }
        public required int Order { get; set; }
        public int CategoryId { get; set; }
    }
}
