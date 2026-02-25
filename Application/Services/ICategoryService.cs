using Application.Inputs;
using ErrorOr;

namespace Application.Services
{
    public interface ICategoryService
    {
        Task CreateCategoryAsync(CreateCategoryRequest request);
        Task<ErrorOr<Updated>> UpdateCategoryAsync(UpdateCategoryRequest request);
    }
}
