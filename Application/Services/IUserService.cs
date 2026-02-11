using Application.Inputs;
using ErrorOr;

namespace Application.Services;

public interface IUserService
{
    Task RegisterUserAsync(RegisterUserRequest registrationRequest);
    Task<ErrorOr<string>> LoginAsync(LoginRequest loginRequest);
}
