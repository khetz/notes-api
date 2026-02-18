using Application.Inputs;
using Application.Outputs;
using Application.Services;
using Domain.Entities;
using ErrorOr;
using Infrastructure.Security;

namespace Infrastructure.Services
{
    public class AuthService : IAuthService
    {
        private readonly JwtService _jwtService;
        private readonly IUserService _userService;

        public AuthService(JwtService jwtService, IUserService userService)
        {
            _jwtService = jwtService;
            _userService = userService;
        }

        public async Task<ErrorOr<string>> LoginAsync(LoginRequest loginRequest)
        {
            var user = await _userService.GetByUsernameAsync(loginRequest.Username);

            if (user.IsError) return user.FirstError;

            var storedHashedPassword = user.Value.PasswordHash;

            if (!PasswordHashingService.VerifyPassword(loginRequest.Password, storedHashedPassword))
                return Error.Unauthorized();

            var token = _jwtService.GenerateToken(user.Value.Id, loginRequest.Username);
            return token;
        }

        public async Task RegisterUserAsync(RegisterUserRequest registrationRequest)
        {
            var user = new User
            {
                Username = registrationRequest.Username,
                PasswordHash = PasswordHashingService.HashPassword(registrationRequest.Password)
            };

            await _userService.AddUserAsync(user);
        }

        public Task<ErrorOr<RefreshTokenResponse>> RefreshAsync(string refreshToken)
        {
            throw new NotImplementedException();
        }

    }
}
