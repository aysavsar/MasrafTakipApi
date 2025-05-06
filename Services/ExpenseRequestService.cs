using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MasrafTakipApi.DTOs;
using MasrafTakipApi.Data;
using MasrafTakipApi.Entities;
using MasrafTakipApi.Interfaces.Service;
using Microsoft.EntityFrameworkCore;

namespace MasrafTakipApi.Services
{
    public class ExpenseRequestService : IExpenseRequestService
    {
        private readonly AppDbContext _db;
        private readonly IMapper _mapper;

        public ExpenseRequestService(AppDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ExpenseRequestDto>> GetAllAsync(
            Guid userId, bool isAdmin,
            ExpenseStatus? status, DateTime? from, DateTime? to)
        {
            var query = _db.ExpenseRequests
                           .Include(e => e.Category)
                           .Include(e => e.User)
                           .AsQueryable();

            if (!isAdmin) query = query.Where(e => e.UserId == userId);
            if (status.HasValue) query = query.Where(e => e.Status == status);
            if (from.HasValue) query = query.Where(e => e.Date >= from.Value);
            if (to.HasValue) query = query.Where(e => e.Date <= to.Value);

            var list = await query.ToListAsync();
            return _mapper.Map<IEnumerable<ExpenseRequestDto>>(list);
        }

        public async Task<ExpenseRequestDto> CreateAsync(
            Guid userId,
            ExpenseRequestCreateDto dto)
        {
            var entity = new ExpenseRequest
            {
                Id = Guid.NewGuid(),
                UserId = userId,
                CategoryId = dto.CategoryId,
                Amount = dto.Amount,
                Date = dto.Date,
                Status = ExpenseStatus.Pending
            };
            _db.ExpenseRequests.Add(entity);
            await _db.SaveChangesAsync();
            return _mapper.Map<ExpenseRequestDto>(entity);
        }

        public async Task<ExpenseRequestDto?> GetByIdAsync(Guid id)
        {
            var entity = await _db.ExpenseRequests
                                   .Include(e => e.Category)
                                   .Include(e => e.User)
                                   .FirstOrDefaultAsync(e => e.Id == id);
            return entity == null ? null : _mapper.Map<ExpenseRequestDto>(entity);
        }

        public async Task ApproveAsync(ApprovalDto dto, string performedBy)
        {
            var req = await _db.ExpenseRequests.FindAsync(dto.RequestId)
                      ?? throw new KeyNotFoundException("Talep bulunamadı");
            if (dto.Approve)
            {
                req.Status = ExpenseStatus.Approved;
                req.IsPaid = true;
            }
            else
            {
                req.Status = ExpenseStatus.Rejected;
                req.RejectionReason = dto.Reason;
            }
            await _db.SaveChangesAsync();
        }
    }
}