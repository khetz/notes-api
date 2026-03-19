using Application.Inputs;
using Application.Interfaces;
using Application.Outputs;
using Application.Services;
using Domain.Entities;
using ErrorOr;
using Infrastructure.Configuration;
using Infrastructure.Security;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.Extensions.Options;

namespace Infrastructure.Services
{
    public class AuthService : IAuthService
    {
        private readonly JwtService _jwtService;
        private readonly IUserService _userService;
        private readonly IRefreshTokenRepository _refreshTokenRepository;
        private readonly JwtSettings _jwtSettings;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AuthService(JwtService jwtService, IUserService userService, IRefreshTokenRepository refreshTokenRepository,
            IOptions<JwtSettings> jwtSettings, IHttpContextAccessor httpContextAccessor)
        {
            _jwtService = jwtService;
            _userService = userService;
            _refreshTokenRepository = refreshTokenRepository;
            _jwtSettings = jwtSettings.Value;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<ErrorOr<AccessTokenResponse>> LoginAsync(LoginRequest loginRequest)
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
                Token = refreshToken,
                Username = loginRequest.Username,
                ExpirationDate = DateTime.UtcNow.AddDays(_jwtSettings.RefreshTokenExpirationDays)
            };

            await _refreshTokenRepository.AddAsync(refreshTokenObject);

            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Secure = true, 
                SameSite = SameSiteMode.None,
                Expires = DateTime.UtcNow.AddDays(7)
            };

            _httpContextAccessor.HttpContext?.Response.Cookies.Append("refreshToken", refreshToken, cookieOptions);

            return new AccessTokenResponse { AccessToken = accessToken };
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

        public async Task<ErrorOr<AccessTokenResponse>> RefreshAsync()
        {
            var currentRefreshToken = _httpContextAccessor.HttpContext?.Request.Cookies["refreshToken"];
            if (currentRefreshToken == null) return Error.Unauthorized();

            var storedRefreshToken = await _refreshTokenRepository.GetByTokenAsync(currentRefreshToken);

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

            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.None,
                Expires = DateTime.UtcNow.AddDays(7)
            };

            _httpContextAccessor.HttpContext?.Response.Cookies.Append("refreshToken", refreshToken, cookieOptions);

            return new AccessTokenResponse { AccessToken = accessToken };
        }

        public async Task LogoutAsync()
        {
            var currentRefreshToken = _httpContextAccessor.HttpContext?.Request.Cookies["refreshToken"];

            if (currentRefreshToken != null)
            {
                await _refreshTokenRepository.DeleteAsync(currentRefreshToken);
            }

            _httpContextAccessor.HttpContext?.Response.Cookies.Delete("refreshToken", new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.None
            });
        }
    }
}
