// MasrafTakipApi/Interfaces/Service/IUserService.cs
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MasrafTakipApi.DTOs;

namespace MasrafTakipApi.Interfaces.Service
{
    public interface IUserService
    {
        Task RegisterAsync(UserRegisterDto dto);
        Task<string> LoginAsync(UserLoginDto dto);
        Task<UserDto> GetByIdAsync(Guid id);

        // Yeni eklenen metot:
        Task<IEnumerable<UserDto>> GetAllAsync();
    }
}
