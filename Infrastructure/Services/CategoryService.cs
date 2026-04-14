using Application.Inputs;
using Application.Interfaces;
using Application.Outputs;
using Application.Services;
using ErrorOr;
using Infrastructure.Mappers;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace Infrastructure.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CategoryService(ICategoryRepository categoryRepository
            , IHttpContextAccessor httpContextAccessor)
        {
            _categoryRepository = categoryRepository;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task CreateCategoryAsync(CreateCategoryRequest request)
        {
            var userId = GetUserId();
            var category = request.ToCategory(userId);
            await _categoryRepository.CreateAsync(category);
        }

        public async Task<ErrorOr<Deleted>> DeleteCategoryAsync(int categoryId)
        {
            var userId = GetUserId();
            await _categoryRepository.DeleteAsync(categoryId, userId);
            return Result.Deleted;
        }

        public async Task<ErrorOr<IReadOnlyCollection<CategoryResponse>>> GetCategoriesAsync()
        {
            var userId = GetUserId();
            var categories = await _categoryRepository.GetAllAsync(userId);
            return categories.Select(c => c.ToCategoryResponse()).ToList();
        }

        public async Task<ErrorOr<Updated>> UpdateCategoryAsync(UpdateCategoryRequest request)
        {
            var userId = GetUserId();
            await _categoryRepository.UpdateAsync(request.Id, userId, request.Name);
            return Result.Updated;
        }

        private int GetUserId()
        {
            var user = _httpContextAccessor.HttpContext?.User;
            if (user == null) throw new KeyNotFoundException(nameof(user));

            return int.Parse(user.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "");
        }
    }
}
