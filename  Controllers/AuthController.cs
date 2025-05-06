using System.Threading.Tasks;
using MasrafTakipApi.DTOs;
using MasrafTakipApi.Interfaces.Service;   // <-- eklendi
using Microsoft.AspNetCore.Mvc;

namespace MasrafTakipApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;  // IUserService bulundu

        public AuthController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UserRegisterDto dto)
        {
            await _userService.RegisterAsync(dto);
            return CreatedAtAction(null, null);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserLoginDto dto)
        {
            var token = await _userService.LoginAsync(dto);
            return Ok(new { Token = token });
        }
    }
}
