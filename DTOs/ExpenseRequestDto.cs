using System;

namespace MasrafTakipApi.DTOs
{
    public class ExpenseRequestDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
        public decimal Amount { get; set; }
        public DateTime RequestDate { get; set; }
        public string Status { get; set; } = null!;
        public string? ApproverComment { get; set; }
        public Guid CreatedByUserId { get; set; }
        public int CategoryId { get; set; }

        public Guid UserId { get; set; }
    }
}
