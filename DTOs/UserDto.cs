using System;

namespace MasrafTakipApi.DTOs
{
    public class UserDto
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string UserName { get; set; } = null!;
        public string Role { get; set; } = null!;
        public DateTime CreatedAt { get; set; }
        public bool IsActive { get; set; }
    }
}