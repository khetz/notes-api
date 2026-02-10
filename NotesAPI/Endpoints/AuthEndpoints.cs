using Application.Interfaces;
using Infrastructure.Security;
using Microsoft.AspNetCore.Mvc;
using NotesAPI.Mappers;
using NotesAPI.Requests;

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

        private static IResult LoginHandler([FromBody] LoginRequest loginRequest, [FromServices] JwtService jwtService)
        {
            // fetch hashed password
            var storedHashedPassword = "$hibrgjkb";

            if (!PasswordHashingService.VerifyPassword(loginRequest.Password, storedHashedPassword))
                return Results.Unauthorized();

            var token = jwtService.GenerateToken(1, loginRequest.Username);
            return Results.Ok(token);
        }

        private static async Task<IResult> RegistrationHandler([FromBody] RegisterUserRequest registrationRequest, [FromServices] IUserService userService)
        {
            await userService.RegisterUserAsync(registrationRequest.ToApplicationRegisterUserRequest());

            return Results.Ok();
        }
    }
}
