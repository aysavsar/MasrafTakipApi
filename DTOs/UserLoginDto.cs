// MasrafTakipApi/DTOs/UserLoginDto.cs
using System.ComponentModel.DataAnnotations;

namespace MasrafTakipApi.DTOs
{
    public class UserLoginDto
    {
        [Required]
        public string UserName { get; set; } = null!;  // artık mevcut

        [Required]
        public string Password { get; set; } = null!;
    }
}
