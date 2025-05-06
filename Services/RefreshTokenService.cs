using MasrafTakipApi.Entities;
using MasrafTakipApi.Interfaces.Repository;
using MasrafTakipApi.Interfaces.Service;

namespace MasrafTakipApi.Services
{
    public class RefreshTokenService : IRefreshTokenService
    {
        private readonly IRefreshTokenRepository _repository;

        public RefreshTokenService(IRefreshTokenRepository repository)
        {
            _repository = repository;
        }

        public async Task AddAsync(RefreshToken token)
        {
            await _repository.AddAsync(token);
            await _repository.SaveChangesAsync();
        }

        public async Task<RefreshToken?> GetByTokenAsync(string token) => await _repository.GetByTokenAsync(token);

        public async Task DeleteAsync(RefreshToken token)
        {
            _repository.Delete(token);
            await _repository.SaveChangesAsync();
        }

        public Task<(string accessToken, string refreshToken)> RefreshAsync(string refreshToken)
        {
            throw new NotImplementedException();
        }
    }
}
