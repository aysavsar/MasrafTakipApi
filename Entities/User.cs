using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace MasrafTakipApi.Entities
{
    public enum UserRole { Admin, Personnel }

    public class User : IdentityUser<Guid>
    {
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public UserRole Role { get; set; }        // Rol alanı eklendi

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public bool IsActive { get; set; } = true;

        public ICollection<ExpenseRequest> ExpenseRequests { get; set; } = new List<ExpenseRequest>();
    }
}
