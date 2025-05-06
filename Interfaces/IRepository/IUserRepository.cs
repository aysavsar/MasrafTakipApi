using MasrafTakipApi.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MasrafTakipApi.Interfaces.Repository
{
    public interface IUserRepository
    {
        Task<User?> GetByIdAsync(Guid id);          // Nullable tip
        Task<User?> GetByUsernameAsync(string username);  // Nullable tip
        Task<IEnumerable<User>> GetAllAsync();
        Task AddAsync(User user);
        Task UpdateAsync(User user);
        Task DeleteAsync(Guid id);
    }
}
