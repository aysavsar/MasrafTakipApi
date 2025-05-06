using MasrafTakipApi.Entities;
using System;
using System.Threading.Tasks;

namespace MasrafTakipApi.Interfaces.Repository
{
    public interface IDocumentRepository
    {
        Task AddAsync(Document document);
        Task<Document?> GetByIdAsync(Guid id);
    }
}
