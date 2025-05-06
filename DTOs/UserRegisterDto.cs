using System.ComponentModel.DataAnnotations;

namespace MasrafTakipApi.DTOs
{
    public class UserRegisterDto
    {
        [Required, MaxLength(100)]
        public string FirstName { get; set; } = null!;

        [Required, MaxLength(100)]
        public string LastName { get; set; } = null!;

        [Required, EmailAddress]
        public string Email { get; set; } = null!;

        [Required, MaxLength(50)]
        public string UserName { get; set; } = null!;

        [Required, MinLength(8)]
        public string Password { get; set; } = null!;
    }
}