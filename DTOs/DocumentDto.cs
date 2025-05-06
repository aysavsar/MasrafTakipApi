using System;

namespace MasrafTakipApi.Dtos
{
    public class DocumentDto
    {
        public Guid Id { get; set; }
        public Guid ExpenseRequestId { get; set; }
        public string FileName { get; set; } = null!;
        public string FileUrl { get; set; } = null!;
    }
}