using Application.Outputs;
using Domain.Entities;

namespace Application.Interfaces
{
    public interface IRefreshTokenRepository
    {
        Task<RefreshTokenResponse> GetByTokenAsync(string token);
        Task UpdateAsync(RefreshToken refreshToken);
    }
}
