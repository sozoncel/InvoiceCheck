namespace InvoiceCheck.Api.Models
{
    public class InvoiceCheckRequest
    {
        public string InvoiceNumber { get; set; } = null!;
        public string TaxNumber { get; set; } = null!;
    }
}