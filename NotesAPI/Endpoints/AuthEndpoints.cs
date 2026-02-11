using Application.Inputs;
using Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace NotesAPI.Endpoints
{
    public static class AuthEndpoints
    {
        public static void MapAuthEndpoints(this RouteGroupBuilder group)
        {
            var authGroup = group.MapGroup("auth");

            authGroup.MapPost("login", LoginHandler);
            authGroup.MapPost("register", RegistrationHandler);
        }

        private static async Task<IResult> LoginHandler([FromBody] LoginRequest loginRequest, [FromServices] IUserService userService)
        {
            var loginResult = await userService.LoginAsync(loginRequest);

            return loginResult.MatchFirst(
                value => Results.Ok(value),
                firstError => Results.Problem(firstError.ToString()));
        }

        private static async Task<IResult> RegistrationHandler([FromBody] RegisterUserRequest registrationRequest, [FromServices] IUserService userService)
        {
            await userService.RegisterUserAsync(registrationRequest);

            return Results.Ok();
        }
    }
}
