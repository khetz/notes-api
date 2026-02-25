using Application.Inputs;
using Domain.Entities;
using ErrorOr;

namespace Application.Services
{
    public interface ICategoryService
    {
        Task CreateCategoryAsync(CreateCategoryRequest request);
        Task<ErrorOr<Updated>> UpdateCategoryAsync(UpdateCategoryRequest request);
        Task<ErrorOr<IReadOnlyCollection<Category>>> GetCategoriesAsync(bool includeNotes);
    }
}
