using Dapper;
using MasrafTakipApi.DTOs;
using MasrafTakipApi.Interfaces.Service;
using Microsoft.Data.SqlClient;  // Gerekli using direktifi
using Microsoft.Extensions.Configuration;

namespace MasrafTakipApi.Services
{
    public class ExpenseReportService : IExpenseReportService
    {
        private readonly IConfiguration _configuration;

        public ExpenseReportService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public Task<IEnumerable<ExpenseReportDto>> GetExpensesByPeriodAsync(DateTime start, DateTime end)
        {
            throw new NotImplementedException();
        }

        public async Task<List<ExpenseReportDto>> GetReportAsync(DateTime start, DateTime end)
        {
            using var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
            var sql = @"SELECT U.FullName AS UserName, SUM(E.Amount) AS TotalAmount
                        FROM ExpenseRequests E
                        JOIN Users U ON E.UserId = U.Id
                        WHERE E.RequestDate BETWEEN @start AND @end
                        GROUP BY U.FullName";

            var results = await connection.QueryAsync<ExpenseReportDto>(sql, new { start, end });
            return results.ToList();
        }

        public Task<IEnumerable<ExpenseReportDto>> GetTotalExpensesByUserAsync()
        {
            throw new NotImplementedException();
        }
    }
}
