using Application.Inputs;
using Application.Outputs;
using Domain.Entities;
using ErrorOr;

namespace Application.Interfaces.Services
{
    public interface ICategoryService
    {
        Task<ErrorOr<Category>> CreateCategoryAsync(CreateCategoryRequest request);
        Task<ErrorOr<Category>> UpdateCategoryAsync(UpdateCategoryRequest request);
        Task<ErrorOr<IReadOnlyCollection<CategoryResponse>>> GetCategoriesAsync();
        Task<ErrorOr<Deleted>> DeleteCategoryAsync(int categoryId);
        Task<ErrorOr<IReadOnlyCollection<NoteResponse>>> GetNotesByCategoryAsync(int categoryId);
    }
}
