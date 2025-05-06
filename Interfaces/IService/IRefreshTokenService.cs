using MasrafTakipApi.DTOs;
using System.Threading.Tasks;

namespace MasrafTakipApi.Interfaces.Service
{
    public interface IRefreshTokenService
    {
        Task<(string accessToken, string refreshToken)> RefreshAsync(string refreshToken);
    }
}
