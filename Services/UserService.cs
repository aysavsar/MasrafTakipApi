// MasrafTakipApi/Services/UserService.cs
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MasrafTakipApi.DTOs;
using MasrafTakipApi.Entities;
using MasrafTakipApi.Interfaces.Service;
using Microsoft.AspNetCore.Identity;

namespace MasrafTakipApi.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;

        public UserService(UserManager<User> userManager, IMapper mapper)
        {
            _userManager = userManager;
            _mapper = mapper;
        }

        public async Task RegisterAsync(UserRegisterDto dto)
        {
            var user = new User
            {
                UserName  = dto.UserName,
                Email     = dto.Email,
                FirstName = dto.FirstName,
                LastName  = dto.LastName,
                Role      = UserRole.Personnel
            };
            var result = await _userManager.CreateAsync(user, dto.Password);
            if (!result.Succeeded)
                throw new ApplicationException(string.Join("; ", result.Errors.Select(e => e.Description)));
        }

        public async Task<string> LoginAsync(UserLoginDto dto)
        {
            var user = await _userManager.FindByNameAsync(dto.UserName)
                       ?? throw new ApplicationException("Kullanıcı adı veya şifre hatalı");
            if (!await _userManager.CheckPasswordAsync(user, dto.Password))
                throw new ApplicationException("Kullanıcı adı veya şifre hatalı");
            // JWT oluşturma...
            return "jwt_token";
        }

        public async Task<UserDto> GetByIdAsync(Guid id)
        {
            var user = await _userManager.FindByIdAsync(id.ToString())
                       ?? throw new KeyNotFoundException("Kullanıcı bulunamadı");
            return _mapper.Map<UserDto>(user);
        }

        // Yeni eklenen metot:
        public async Task<IEnumerable<UserDto>> GetAllAsync()
        {
            var users = _userManager.Users.ToList();
            return _mapper.Map<IEnumerable<UserDto>>(users);
        }
    }
}
