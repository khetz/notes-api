using Application.Inputs;
using Application.Outputs;
using Application.Services;
using ErrorOr;

namespace Infrastructure.Services
{
    public class AuthService : IAuthService
    {
        public Task<ErrorOr<string>> LoginAsync(LoginRequest loginRequest)
        {
            throw new NotImplementedException();
        }

        public Task<ErrorOr<RefreshTokenResponse>> Refresh(string refreshToken)
        {
            throw new NotImplementedException();
        }

        public Task RegisterUserAsync(RegisterUserRequest registrationRequest)
        {
            throw new NotImplementedException();
        }
    }
}
