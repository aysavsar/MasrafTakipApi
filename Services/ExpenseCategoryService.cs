using MasrafTakipApi.Entities;
using MasrafTakipApi.Interfaces.Repository;
using MasrafTakipApi.Interfaces.Service;

namespace MasrafTakipApi.Services
{
    public class ExpenseCategoryService : IExpenseCategoryService
    {
        private readonly IExpenseCategoryRepository _repository;

        public ExpenseCategoryService(IExpenseCategoryRepository repository)
        {
            _repository = repository;
        }

        public async Task AddAsync(ExpenseCategory category)
        {
            await _repository.AddAsync(category);
            await _repository.SaveChangesAsync();
        }

        public Task CreateAsync(string name)
        {
            throw new NotImplementedException();
        }

        public async Task DeleteAsync(ExpenseCategory category)
        {
            _repository.Delete(category);
            await _repository.SaveChangesAsync();
        }

        public Task DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<ExpenseCategory>> GetAllAsync() => await _repository.GetAllAsync();

        public async Task<ExpenseCategory?> GetByIdAsync(int id) => await _repository.GetByIdAsync(id);

        public async Task UpdateAsync(ExpenseCategory category)
        {
            _repository.Update(category);
            await _repository.SaveChangesAsync();
        }

        public Task UpdateAsync(int id, string name)
        {
            throw new NotImplementedException();
        }
    }
}
