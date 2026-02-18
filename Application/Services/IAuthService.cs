using Application.Inputs;
using Application.Outputs;
using ErrorOr;

namespace Application.Services
{
    public interface IAuthService
    {
        Task RegisterUserAsync(RegisterUserRequest registrationRequest);
        Task<ErrorOr<string>> LoginAsync(LoginRequest loginRequest);
        Task<ErrorOr<RefreshTokenResponse>> RefreshAsync(RefreshTokenRequest refreshTokenRequest);
    }
}
