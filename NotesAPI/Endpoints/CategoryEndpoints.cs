using Application.Inputs;
using Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace NotesAPI.Endpoints
{
    public static class CategoryEndpoints
    {
        public static void MapCategoryEndpoints(this RouteGroupBuilder routeBuilder)
        {
            var group = routeBuilder.MapGroup("categories");

            group.MapPost("", CreateCategoryHandler);
            group.MapPatch("/{id}", UpdateCategoryHandler);
            group.MapGet("", GetAllCategoriesHandler);
        }

        private async static Task CreateCategoryHandler([FromBody] CreateCategoryRequest request,
            [FromServices] ICategoryService categoryService)
        {
            await categoryService.CreateCategoryAsync(request);
        }

        private async static Task<IResult> UpdateCategoryHandler([FromBody] UpdateCategoryRequest request,
            [FromServices] ICategoryService categoryService)
        {
            // TODO: add id validation

            var updateResult = await categoryService.UpdateCategoryAsync(request);
            return updateResult.MatchFirst(
                value => Results.Ok(value),
                firstError => Results.Problem(firstError.ToString()));
        }

        private async static Task<IResult> GetAllCategoriesHandler([FromQuery] bool includeNotes,
            [FromServices] ICategoryService categoryService)
        {
            var categories = await categoryService.GetCategoriesAsync(includeNotes);
            return categories.MatchFirst(
                value => Results.Ok(value),
                firstError => Results.Problem(firstError.ToString()));
        }
    }
}
