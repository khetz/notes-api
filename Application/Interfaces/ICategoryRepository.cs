using Domain.Entities;

namespace Application.Interfaces
{
    public interface ICategoryRepository
    {
        Task CreateAsync(Category category);
    }
}
