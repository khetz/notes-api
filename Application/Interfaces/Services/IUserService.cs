using Application.Inputs;
using Domain.Entities;
using ErrorOr;

namespace Application.Interfaces.Services;

public interface IUserService
{
    Task<ErrorOr<User>> GetByUsernameAsync(string username);
    Task<ErrorOr<Created>> AddUserAsync(User user);
}
