using Application.Inputs;

namespace Application.Services
{
    public interface ICategoryService
    {
        Task CreateCategoryAsync(CreateCategoryRequest request);
    }
}
