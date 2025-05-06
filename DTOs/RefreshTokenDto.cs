using System.ComponentModel.DataAnnotations;

namespace MasrafTakipApi.DTOs
{
    public class RefreshTokenDto
    {
        [Required] public string RefreshToken { get; set; }
    }
}
