using MasrafTakipApi.DTOs;
using System.Threading.Tasks;

namespace MasrafTakipApi.Interfaces.Service
{
    public interface IAuthService
    {
        Task<string> LoginAsync(UserLoginDto dto);
        Task<(string accessToken, string refreshToken)> RefreshAsync(string refreshToken);
    }
}
