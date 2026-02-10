using Application.Inputs;
using Application.Interfaces;
using Domain.Entities;
using Infrastructure.Security;

namespace Infrastructure.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task RegisterUserAsync(RegisterUserRequest registrationRequest)
        {
            var user = new User
            {
                Username = registrationRequest.Username,
                PasswordHash = PasswordHashingService.HashPassword(registrationRequest.Password)
            };

            await _userRepository.AddAsync(user);
        }
    }
}
