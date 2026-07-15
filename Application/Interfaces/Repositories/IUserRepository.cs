using Domain.Entities;
using ErrorOr;

namespace Application.Interfaces.Repositories
{
    public interface IUserRepository
    {
        Task<ErrorOr<Created>> AddAsync(User user);
        Task<ErrorOr<User>> GetByUsernameAsync(string username);
    }
}
