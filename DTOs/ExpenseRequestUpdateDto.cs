using System;
using System.ComponentModel.DataAnnotations;

namespace MasrafTakipApi.DTOs
{
    public class ExpenseRequestUpdateDto
    {
        [Required]
        public Guid Id { get; set; }

        [Required]
        public Guid CategoryId { get; set; }

        [Required, Range(0.01, double.MaxValue)]
        public decimal Amount { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        public string Status { get; set; } = null!; // "Pending", "Approved", "Rejected", "Paid"

        public string? RejectionReason { get; set; }
    }
}