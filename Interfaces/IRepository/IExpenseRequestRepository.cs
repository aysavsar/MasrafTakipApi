using MasrafTakipApi.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MasrafTakipApi.Interfaces.Repository
{
    public interface IExpenseRequestRepository
    {
        Task<List<ExpenseRequest>> GetAllAsync();
        Task<ExpenseRequest?> GetByIdAsync(Guid id);
        Task<List<ExpenseRequest>> GetByUserIdAsync(Guid userId);
        Task AddAsync(ExpenseRequest request);
        void Update(ExpenseRequest request);
        void Delete(ExpenseRequest request);
        Task SaveChangesAsync();
    }
}
