using Domain.Entities;
using ErrorOr;

namespace Application.Interfaces
{
    public interface IUserRepository
    {
        Task AddAsync(User user);
        Task<ErrorOr<User>> GetByUsernameAsync(string username);
    }
}
