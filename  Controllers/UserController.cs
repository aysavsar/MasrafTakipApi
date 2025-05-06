using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MasrafTakipApi.Interfaces.Service;
using MasrafTakipApi.DTOs;

namespace MasrafTakipApi.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _svc;
        public UserController(IUserService svc) => _svc = svc;

        [HttpGet, Authorize(Roles="Admin")]
        public async Task<IActionResult> GetAll() => Ok(await _svc.GetAllAsync());

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id) =>
            Ok(await _svc.GetByIdAsync(id));
    }
}
