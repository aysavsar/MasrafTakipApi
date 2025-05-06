using MasrafTakipApi.Data;
using MasrafTakipApi.Entities;
using MasrafTakipApi.Interfaces.Repository;
using Microsoft.EntityFrameworkCore;

namespace MasrafTakipApi.Repositories
{
    public class ExpenseCategoryRepository : IExpenseCategoryRepository
    {
        private readonly AppDbContext _context;
        public ExpenseCategoryRepository(AppDbContext context) => _context = context;

        public async Task AddAsync(ExpenseCategory category) => await _context.ExpenseCategories.AddAsync(category);
        public void Delete(ExpenseCategory category) => _context.ExpenseCategories.Remove(category);
        public async Task<List<ExpenseCategory>> GetAllAsync() => await _context.ExpenseCategories.ToListAsync();
        public async Task<ExpenseCategory?> GetByIdAsync(int id) => await _context.ExpenseCategories.FindAsync(id);
        public async Task SaveChangesAsync() => await _context.SaveChangesAsync();
        public void Update(ExpenseCategory category) => _context.ExpenseCategories.Update(category);
    }
}
