using Application.Interfaces.Repositories;
using Application.Interfaces.Services;
using Domain.Entities;
using ErrorOr;

namespace Infrastructure.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        
        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<ErrorOr<User>> GetByUsernameAsync(string username)
        {
            return await _userRepository.GetByUsernameAsync(username);
        }

        public async Task<ErrorOr<Created>> AddUserAsync(User user)
        {
            return await _userRepository.AddAsync(user);
        }
    }
}
