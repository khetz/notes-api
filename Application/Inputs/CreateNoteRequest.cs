namespace Application.Inputs;

public class CreateNoteRequest
{
    public required string Title { get; set; }
    public required string Content { get; set; }
    public required int CategoryId { get; set; }
}
