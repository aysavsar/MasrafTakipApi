using MasrafTakipApi.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MasrafTakipApi.Interfaces.Repository
{
    public interface IExpenseCategoryRepository
    {
        Task<List<ExpenseCategory>> GetAllAsync();
        Task<ExpenseCategory?> GetByIdAsync(int id);
        Task AddAsync(ExpenseCategory category);
        void Update(ExpenseCategory category);
        void Delete(ExpenseCategory category);
        Task SaveChangesAsync();
    }
}
