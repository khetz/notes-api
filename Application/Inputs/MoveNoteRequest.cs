namespace Application.Inputs
{
    public class MoveNoteRequest
    {
        public int Id { get; set; }
        public int Order { get; set; }
        public int CategoryId { get; set; }
    }
}
