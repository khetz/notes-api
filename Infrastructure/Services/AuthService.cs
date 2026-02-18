using Application.Inputs;
using Application.Interfaces;
using Application.Outputs;
using Application.Services;
using Domain.Entities;
using ErrorOr;
using Infrastructure.Configuration;
using Infrastructure.Security;
using Microsoft.Extensions.Options;

namespace Infrastructure.Services
{
    public class AuthService : IAuthService
    {
        private readonly JwtService _jwtService;
        private readonly IUserService _userService;
        private readonly IRefreshTokenRepository _refreshTokenRepository;
        private readonly JwtSettings _jwtSettings;

        public AuthService(JwtService jwtService, IUserService userService, IRefreshTokenRepository refreshTokenRepository,
            IOptions<JwtSettings> jwtSettings)
        {
            _jwtService = jwtService;
            _userService = userService;
            _refreshTokenRepository = refreshTokenRepository;
            _jwtSettings = jwtSettings.Value;
        }

        public async Task<ErrorOr<RefreshTokenResponse>> LoginAsync(LoginRequest loginRequest)
        {
            var user = await _userService.GetByUsernameAsync(loginRequest.Username);

            if (user.IsError) return user.FirstError;

            var storedHashedPassword = user.Value.PasswordHash;

            if (!PasswordHashingService.VerifyPassword(loginRequest.Password, storedHashedPassword))
                return Error.Unauthorized();

            var accessToken = _jwtService.GenerateToken(user.Value.Id, loginRequest.Username);
            var refreshToken = _jwtService.GenerateRefreshToken();
            var refreshTokenObject = new RefreshToken
            {
                Token = accessToken,
                Username = loginRequest.Username,
                ExpirationDate = DateTime.UtcNow.AddDays(_jwtSettings.RefreshTokenExpirationDays)
            };

            await _refreshTokenRepository.AddAsync(refreshTokenObject);

            return new RefreshTokenResponse { AccessToken = accessToken, RefreshToken = refreshToken };
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

        public async Task<ErrorOr<RefreshTokenResponse>> RefreshAsync(RefreshTokenRequest refreshTokenRequest)
        {
            var storedRefreshToken = await _refreshTokenRepository.GetByTokenAsync(refreshTokenRequest.RefreshToken);

            if (storedRefreshToken.FirstError == Error.NotFound() || storedRefreshToken.Value.ExpirationDate < DateTime.UtcNow)
                return Error.Unauthorized();

            var storedRefreshTokenValue = storedRefreshToken.Value;
            var user = await _userService.GetByUsernameAsync(storedRefreshTokenValue.Username);
            if (user.IsError) return Error.NotFound();

            var userValue = user.Value;

            var accessToken = _jwtService.GenerateToken(userValue.Id, userValue.Username);
            var refreshToken = _jwtService.GenerateRefreshToken();

            storedRefreshTokenValue.Token = refreshToken;
            storedRefreshTokenValue.ExpirationDate = DateTime.UtcNow.AddDays(_jwtSettings.RefreshTokenExpirationDays);
            await _refreshTokenRepository.UpdateAsync(storedRefreshTokenValue);

            return new RefreshTokenResponse { AccessToken = accessToken, RefreshToken = refreshToken };
        }
    }
}
