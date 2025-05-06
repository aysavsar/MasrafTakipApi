using MasrafTakipApi.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MasrafTakipApi.Interfaces.Service
{
    public interface IExpenseReportService
    {
        Task<IEnumerable<ExpenseReportDto>> GetTotalExpensesByUserAsync();
        Task<IEnumerable<ExpenseReportDto>> GetExpensesByPeriodAsync(DateTime start, DateTime end);
    }
}
