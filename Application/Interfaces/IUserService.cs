using Application.Inputs;

namespace Application.Interfaces;

public interface IUserService
{
    Task RegisterUserAsync(RegisterUserRequest registrationRequest);
}
