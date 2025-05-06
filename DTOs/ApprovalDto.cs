using System;
using System.ComponentModel.DataAnnotations;

namespace MasrafTakipApi.DTOs
{
    public class ApprovalDto
    {
        [Required]
        public Guid RequestId { get; set; }

        [Required]
        public bool Approve { get; set; }

        public string? Reason { get; set; }
    }
}