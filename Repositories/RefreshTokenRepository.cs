using MasrafTakipApi.Data;
using MasrafTakipApi.Entities;
using MasrafTakipApi.Interfaces.Repository;
using Microsoft.EntityFrameworkCore;

namespace MasrafTakipApi.Repositories
{
    public class RefreshTokenRepository : IRefreshTokenRepository
    {
        private readonly AppDbContext _context;
        public RefreshTokenRepository(AppDbContext context) => _context = context;

        public async Task AddAsync(RefreshToken token) => await _context.RefreshTokens.AddAsync(token);
        public async Task<RefreshToken?> GetByTokenAsync(string token) => await _context.RefreshTokens.FirstOrDefaultAsync(rt => rt.Token == token);
        public void Delete(RefreshToken token) => _context.RefreshTokens.Remove(token);
        public async Task SaveChangesAsync() => await _context.SaveChangesAsync();
    }
}
