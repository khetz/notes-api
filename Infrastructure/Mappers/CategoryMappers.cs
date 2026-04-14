using Application.Inputs;
using Application.Outputs;
using Domain.Entities;

namespace Infrastructure.Mappers
{
    public static class CategoryMappers
    {
        public static Category ToCategory(this CreateCategoryRequest request, int userId) => new()
        {
            Name = request.Name,
            UserId = userId,
            User = null
        };

        public static CategoryResponse ToCategoryResponse(this Category category) => new()
        {
            Name = category.Name,
            Id = category.Id
        };
    }
}
