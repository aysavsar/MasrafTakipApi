using System;
using System.ComponentModel.DataAnnotations;

namespace MasrafTakipApi.DTOs
{
    public class ExpenseRequestCreateDto
    {
        [Required]
        public Guid CategoryId { get; set; }

        [Required, Range(0.01, double.MaxValue)]
        public decimal Amount { get; set; }

        [Required]
        public DateTime Date { get; set; }
    }
}