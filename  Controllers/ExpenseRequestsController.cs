using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using MasrafTakipApi.DTOs;
using MasrafTakipApi.Entities;
using MasrafTakipApi.Interfaces.Service;   // <-- eklendi
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MasrafTakipApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ExpenseRequestsController : ControllerBase
    {
        private readonly IExpenseRequestService _service;

        public ExpenseRequestsController(IExpenseRequestService service)
        {
            _service = service;
        }

        [HttpGet]
        [Authorize]
        public async Task<IEnumerable<ExpenseRequestDto>> GetAll(
            [FromQuery] ExpenseStatus? status,
            [FromQuery] DateTime? fromDate,
            [FromQuery] DateTime? toDate)
        {
            var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
            var isAdmin = User.IsInRole(UserRole.Admin.ToString());
            return await _service.GetAllAsync(userId, isAdmin, status, fromDate, toDate);
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<ExpenseRequestDto>> GetById(Guid id)
        {
            var dto = await _service.GetByIdAsync(id);
            if (dto == null) return NotFound();

            var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
            if (!User.IsInRole(UserRole.Admin.ToString()) && dto.UserId != userId)
                return Forbid();

            return Ok(dto);
        }

        [HttpPost]
        [Authorize(Policy = "RequirePersonnel")]
        public async Task<IActionResult> Create([FromBody] ExpenseRequestCreateDto dto)
        {
            var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
            var created = await _service.CreateAsync(userId, dto);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        [HttpPost("approve")]
        [Authorize(Policy = "RequireAdmin")]
        public async Task<IActionResult> Approve([FromBody] ApprovalDto dto)
        {
            await _service.ApproveAsync(dto, User.Identity!.Name!);
            return NoContent();
        }
    }
}
