using System;

namespace MasrafTakipApi.Entities
{
    public class RefreshToken
    {
        public Guid Id { get; set; }
        public required string Token { get; set; }
        public DateTime Expires { get; set; }
        public Guid UserId { get; set; }
        public required User User { get; set; }
    }
}