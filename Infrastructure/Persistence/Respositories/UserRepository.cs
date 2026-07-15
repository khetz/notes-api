using Application.Interfaces.Repositories;
using Domain.Entities;
using ErrorOr;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Respositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _dbContext;

        public UserRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ErrorOr<Created>> AddAsync(User user)
        {
            _dbContext.Users.Add(user);
            await _dbContext.SaveChangesAsync();

            return Result.Created;
        }

        public async Task<ErrorOr<User>> GetByUsernameAsync(string username)
        {
            var dbUser = await _dbContext.Users
                .Where(x => x.Username == username).FirstOrDefaultAsync();

            if (dbUser == null) return Error.NotFound();

            return dbUser;
        }
    }
}
