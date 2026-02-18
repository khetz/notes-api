using Application.Inputs;
using Domain.Entities;
using ErrorOr;

namespace Application.Services;

public interface IUserService
{
    Task<ErrorOr<User>> GetByUsernameAsync(string username);
    Task<ErrorOr<Created>> AddUserAsync(User user);
}
