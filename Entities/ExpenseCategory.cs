using System;
using System.ComponentModel.DataAnnotations;

namespace MasrafTakipApi.Entities
{
    public class ExpenseCategory
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Kategori adı zorunludur")]
        [MaxLength(100, ErrorMessage = "Kategori adı en fazla 100 karakter olabilir")]
        public required string Name { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }
        public bool IsActive { get; set; } = true;
    }
}