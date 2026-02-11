namespace Infrastructure.Models
{
    public class CategoryDbModel
    {
        public int Id { get; set; }
        public required string Name { get; set; }

        public ICollection<NoteDbModel> Notes { get; set; } = [];
    }
}
