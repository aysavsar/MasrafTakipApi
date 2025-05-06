using System;
using System.Collections.Generic;

namespace MasrafTakipApi.Entities
{
    public enum ExpenseStatus { Pending, Approved, Rejected, Paid }

    public class ExpenseRequest
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public User User { get; set; } = null!;

        public Guid CategoryId { get; set; }
        public ExpenseCategory Category { get; set; } = null!;

        public decimal Amount { get; set; }
        public DateTime Date { get; set; }

        public ExpenseStatus Status { get; set; } = ExpenseStatus.Pending;
        public string? RejectionReason { get; set; }   // Red sebebi
        public bool IsPaid { get; set; } = false;      // Ödeme durumu

        public ICollection<Document> Documents { get; set; } = new List<Document>();
    }
}
