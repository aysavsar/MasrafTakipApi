using MasrafTakipApi.Data;
using MasrafTakipApi.Entities;
using MasrafTakipApi.Interfaces.Repository;
using Microsoft.EntityFrameworkCore;

namespace MasrafTakipApi.Repositories
{
    public class DocumentRepository : IDocumentRepository
    {
        private readonly AppDbContext _context;
        public DocumentRepository(AppDbContext context) => _context = context;

        public async Task AddAsync(Document document) => await _context.Documents.AddAsync(document);
        public async Task<Document?> GetByIdAsync(Guid id) => await _context.Documents.FindAsync(id);
    }
}
