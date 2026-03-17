using Application.Inputs;
using Application.Outputs;
using ErrorOr;

namespace Application.Services
{
    public interface IAuthService
    {
        Task RegisterUserAsync(RegisterUserRequest registrationRequest);
        Task<ErrorOr<AccessTokenResponse>> LoginAsync(LoginRequest loginRequest);
        Task<ErrorOr<AccessTokenResponse>> RefreshAsync(RefreshTokenRequest refreshTokenRequest);
    }
}
