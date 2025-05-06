using System;

namespace MasrafTakipApi.Entities
{
    public class Document
    {
        public Guid Id { get; set; }
        public string FileName { get; set; } = null!;
        public byte[] Content { get; set; } = null!;
        public Guid ExpenseRequestId { get; set; }
        public ExpenseRequest ExpenseRequest { get; set; } = null!;
    }
}