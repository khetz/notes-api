using Application.Inputs;
using Application.Interfaces;
using Application.Services;
using Domain.Entities;
using ErrorOr;
using Infrastructure.Security;

namespace Infrastructure.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly JwtService _jwtService;

        public UserService(IUserRepository userRepository, JwtService jwtService)
        {
            _userRepository = userRepository;
            _jwtService = jwtService;
        }

        public async Task<ErrorOr<string>> LoginAsync(LoginRequest loginRequest)
        {
            var user = await _userRepository.GetByUsernameAsync(loginRequest.Username);

            if (user.IsError) return user.FirstError;

            var storedHashedPassword = user.Value.PasswordHash;

            if (!PasswordHashingService.VerifyPassword(loginRequest.Password, storedHashedPassword))
                return Error.Unauthorized();

            var token = _jwtService.GenerateToken(user.Value.Id, loginRequest.Username);
            return token;
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
