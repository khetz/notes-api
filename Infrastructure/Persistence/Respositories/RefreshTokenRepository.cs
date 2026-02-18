using Application.Interfaces;
using Application.Outputs;
using Domain.Entities;

namespace Infrastructure.Persistence.Respositories
{
    public class RefreshTokenRepository : IRefreshTokenRepository
    {
        public Task<RefreshTokenResponse> GetByTokenAsync(string token)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(RefreshToken refreshToken)
        {
            throw new NotImplementedException();
        }
    }
}
