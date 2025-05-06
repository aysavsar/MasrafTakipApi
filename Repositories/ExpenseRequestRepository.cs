using MasrafTakipApi.Data;
using MasrafTakipApi.Entities;
using MasrafTakipApi.Interfaces.Repository;
using Microsoft.EntityFrameworkCore;

namespace MasrafTakipApi.Repositories
{
    public class ExpenseRequestRepository : IExpenseRequestRepository
    {
        private readonly AppDbContext _context;
        public ExpenseRequestRepository(AppDbContext context) => _context = context;

        public async Task AddAsync(ExpenseRequest request) => await _context.ExpenseRequests.AddAsync(request);

        public void Delete(ExpenseRequest request) => _context.ExpenseRequests.Remove(request);

        public async Task<List<ExpenseRequest>> GetAllAsync() =>
            await _context.ExpenseRequests
                .Include(x => x.Category)
                .Include(x => x.User)
                .ToListAsync();

        public async Task<ExpenseRequest?> GetByIdAsync(Guid id) =>
            await _context.ExpenseRequests
                .Include(x => x.Category)
                .Include(x => x.User)
                .FirstOrDefaultAsync(x => x.Id == id);

        public async Task<List<ExpenseRequest>> GetByUserIdAsync(Guid userId) =>
            await _context.ExpenseRequests
                .Where(x => x.UserId == userId)
                .Include(x => x.Category)
                .ToListAsync();

        public async Task SaveChangesAsync() => await _context.SaveChangesAsync();

        public void Update(ExpenseRequest request) => _context.ExpenseRequests.Update(request);
    }
}
