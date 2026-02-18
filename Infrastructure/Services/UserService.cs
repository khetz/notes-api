using Application.Inputs;
using Application.Interfaces;
using Application.Services;
using Domain.Entities;
using ErrorOr;
using Infrastructure.Security;
using Microsoft.AspNetCore.Identity.Data;

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

        public async Task<ErrorOr<User>> GetByUsernameAsync(string username)
        {
            return await _userRepository.GetByUsernameAsync(username);
        }
    }
}
