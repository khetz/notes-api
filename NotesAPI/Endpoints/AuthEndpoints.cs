using Application.Inputs;
using Application.Outputs;
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
            authGroup.MapPost("refresh", RefreshHandler);
        }

        private static async Task<IResult> LoginHandler([FromBody] LoginRequest loginRequest, [FromServices] IAuthService authService)
        {
            var loginResult = await authService.LoginAsync(loginRequest);

            return loginResult.MatchFirst(
                value => Results.Ok(value),
                firstError => Results.Problem(firstError.ToString()));
        }

        private static async Task<IResult> RegistrationHandler([FromBody] RegisterUserRequest registrationRequest,
            [FromServices] IAuthService authService)
        {
            await authService.RegisterUserAsync(registrationRequest);

            return Results.Ok();
        }

        private static async Task<IResult> RefreshHandler([FromBody] RefreshTokenRequest refreshTokenRequest,
            [FromServices] IAuthService authService)
        {
            var refreshTokenObject = await authService.RefreshAsync(refreshToken);

            return refreshTokenObject.MatchFirst(
                value => Results.Ok(value),
                firstError => Results.Problem(firstError.ToString()));
        }
    }
}
