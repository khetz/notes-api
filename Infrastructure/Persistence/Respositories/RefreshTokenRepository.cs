using Application.Interfaces;
using Domain.Entities;
using ErrorOr;

namespace Infrastructure.Persistence.Respositories
{
    public class RefreshTokenRepository : IRefreshTokenRepository
    {
        public Task<ErrorOr<RefreshToken>> GetByTokenAsync(string token)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(RefreshToken refreshToken)
        {
            throw new NotImplementedException();
        }
    }
}
