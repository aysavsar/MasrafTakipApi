using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MasrafTakipApi.DTOs;
using MasrafTakipApi.Entities;

namespace MasrafTakipApi.Interfaces.Service
{
    public interface IExpenseRequestService
    {
        Task<IEnumerable<ExpenseRequestDto>> GetAllAsync(
            Guid userId, bool isAdmin,
            ExpenseStatus? status = null,
            DateTime? from = null,
            DateTime? to = null);

        Task<ExpenseRequestDto> CreateAsync(
            Guid userId,
            ExpenseRequestCreateDto dto);

        Task<ExpenseRequestDto?> GetByIdAsync(Guid id);
        Task ApproveAsync(ApprovalDto dto, string performedBy);
    }
}