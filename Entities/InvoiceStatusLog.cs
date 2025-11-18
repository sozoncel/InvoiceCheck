using System;

namespace InvoiceCheck.Api.Entities
{
    public class InvoiceStatusLog
    {
        public long Id { get; set; }
        public string InvoiceNumber { get; set; } = null!;
        public string TaxNumber { get; set; } = null!;
        public string ResponseCode { get; set; } = null!;
        public string ResponseMessage { get; set; } = null!;
        public DateTime RequestTime { get; set; }
    }
}