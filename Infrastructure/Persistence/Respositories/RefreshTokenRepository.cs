using Application.Interfaces;
using Domain.Entities;
using ErrorOr;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Respositories
{
    public class RefreshTokenRepository : IRefreshTokenRepository
    {
        private readonly AppDbContext _appDbContext;

        public RefreshTokenRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task AddAsync(RefreshToken refreshToken)
        {
            await _appDbContext.RefreshTokens.AddAsync(refreshToken);
            await _appDbContext.SaveChangesAsync();
        }

        public async Task<ErrorOr<RefreshToken>> GetByTokenAsync(string token)
        {
            var refreshToken = await _appDbContext.RefreshTokens
                .FirstOrDefaultAsync(rt => rt.Token == token);

            if (refreshToken == null) return Error.NotFound();

            return refreshToken; 
        }

        public async Task UpdateAsync(RefreshToken refreshToken)
        {
            _appDbContext.RefreshTokens.Update(refreshToken);
            await _appDbContext.SaveChangesAsync();
        }
    }
}
