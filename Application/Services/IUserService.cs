using Application.Inputs;

namespace Application.Services;

public interface IUserService
{
    Task RegisterUserAsync(RegisterUserRequest registrationRequest);
}
