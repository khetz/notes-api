namespace Application.Outputs
{
    public class CategoryResponse
    {
        public int Id { get; set; }
        public required string Name { get; set; }

        public ICollection<NoteSummaryResponse> Notes { get; set; } = [];
    }
}
