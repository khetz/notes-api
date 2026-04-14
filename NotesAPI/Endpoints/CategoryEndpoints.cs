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
            group.MapPatch("{id}", UpdateCategoryHandler);
            group.MapGet("", GetAllCategoriesHandler);
            group.MapDelete("{id}", DeleteCategoryHandler);
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

        private async static Task<IResult> GetAllCategoriesHandler(
            [FromServices] ICategoryService categoryService)
        {
            var categories = await categoryService.GetCategoriesAsync();
            return categories.MatchFirst(
                value => Results.Ok(value),
                firstError => Results.Problem(firstError.ToString()));
        }

        private async static Task<IResult> DeleteCategoryHandler([FromRoute] int id,
            [FromServices] ICategoryService categoryService)
        {
            var deletionResult = await categoryService.DeleteCategoryAsync(id);
            return deletionResult.MatchFirst(
                value => Results.Ok(value),
                firstError => Results.Problem(firstError.ToString()));
        }

        private async static Task<IResult> GetNotesByCategory(
            [FromRoute] int id,
            [FromServices] ICategoryService categoryService)
        {

        }
    }
}
