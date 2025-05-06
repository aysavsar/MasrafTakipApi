using MasrafTakipApi.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MasrafTakipApi.Interfaces.Service
{
    public interface IExpenseCategoryService
    {
        Task<List<ExpenseCategory>> GetAllAsync();
        Task<ExpenseCategory?> GetByIdAsync(int id);
        Task CreateAsync(string name);
        Task UpdateAsync(int id, string name);
        Task DeleteAsync(int id);
    }
}
