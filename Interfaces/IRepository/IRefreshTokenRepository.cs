using MasrafTakipApi.Entities;
using System;
using System.Threading.Tasks;

namespace MasrafTakipApi.Interfaces.Repository
{
    public interface IRefreshTokenRepository
    {
        Task AddAsync(RefreshToken token);
        Task<RefreshToken?> GetByTokenAsync(string token);
        void Delete(RefreshToken token);
        Task SaveChangesAsync();
    }
}
