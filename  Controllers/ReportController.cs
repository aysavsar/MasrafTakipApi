using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MasrafTakipApi.Interfaces.Service;

namespace MasrafTakipApi.Controllers
{
    [ApiController]
    [Authorize(Roles="Admin")]
    [Route("api/[controller]")]
    public class ReportController : ControllerBase
    {
        private readonly IExpenseReportService _svc;
        public ReportController(IExpenseReportService svc) => _svc = svc;

        [HttpGet("by-user")]
        public async Task<IActionResult> ByUser() =>
            Ok(await _svc.GetTotalExpensesByUserAsync());
    }
}
