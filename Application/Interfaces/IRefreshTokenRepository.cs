using Domain.Entities;
using ErrorOr;

namespace Application.Interfaces
{
    public interface IRefreshTokenRepository
    {
        Task<ErrorOr<RefreshToken>> GetByTokenAsync(string token);
        Task UpdateAsync(RefreshToken refreshToken);
        Task AddAsync(RefreshToken refreshToken);
    }
}
