using Application.Inputs;
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
    }
}
