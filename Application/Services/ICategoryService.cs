using Application.Inputs;
using Application.Outputs;
using ErrorOr;

namespace Application.Services
{
    public interface ICategoryService
    {
        Task CreateCategoryAsync(CreateCategoryRequest request);
        Task<ErrorOr<Updated>> UpdateCategoryAsync(UpdateCategoryRequest request);
        Task<ErrorOr<IReadOnlyCollection<CategoryResponse>>> GetCategoriesAsync(bool includeNotes);
        Task<ErrorOr<Deleted>> DeleteCategoryAsync(int categoryId);
    }
}
