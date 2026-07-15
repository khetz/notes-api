using Domain.Entities;
using ErrorOr;

namespace Application.Interfaces.Repositories
{
    public interface IRefreshTokenRepository
    {
        Task<ErrorOr<RefreshToken>> GetByTokenAsync(string token);
        Task UpdateAsync(RefreshToken refreshToken);
        Task AddAsync(RefreshToken refreshToken);
        Task<ErrorOr<Deleted>> DeleteAsync(string token);
    }
}
