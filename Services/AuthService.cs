using MasrafTakipApi.DTOs;
using MasrafTakipApi.Entities;
using MasrafTakipApi.Interfaces.Repository;
using MasrafTakipApi.Interfaces.Service;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MasrafTakipApi.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly IConfiguration _configuration;

        public AuthService(IUserRepository userRepository, IConfiguration configuration)
        {
            _userRepository = userRepository;
            _configuration = configuration;
        }

        public async Task<string?> AuthenticateAsync(string username, string password)
        {
            var user = await _userRepository.GetByUsernameAsync(username);
            if (user == null || user.PasswordHash != password) // PasswordHash ile karşılaştırma
                return null;

            var tokenHandler = new JwtSecurityTokenHandler();

            // JWT Key'inin null olmaması gerektiğini kontrol et
            var key = _configuration["Jwt:Key"];
            if (string.IsNullOrEmpty(key))
                throw new InvalidOperationException("JWT Key not configured.");

            var keyBytes = Encoding.ASCII.GetBytes(key); // Key'i ASCII formatında aldık
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new(ClaimTypes.Role, user.Role.ToString())  // Role enum'u Claim olarak eklendi ve string'e çevrildi
                }),
                Expires = DateTime.UtcNow.AddMinutes(30),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(keyBytes), SecurityAlgorithms.HmacSha256Signature),
                Issuer = _configuration["Jwt:Issuer"],
                Audience = _configuration["Jwt:Audience"]
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public Task<string> LoginAsync(UserLoginDto dto)
        {
            throw new NotImplementedException();
        }

        public Task<(string accessToken, string refreshToken)> RefreshAsync(string refreshToken)
        {
            throw new NotImplementedException();
        }
    }
}
