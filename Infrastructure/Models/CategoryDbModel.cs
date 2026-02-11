using Domain.Entities;

namespace Infrastructure.Models
{
    public class CategoryDbModel
    {
        public int Id { get; set; }
        public required string Name { get; set; }

        public ICollection<Note> Notes { get; set; } = [];
    }
}
