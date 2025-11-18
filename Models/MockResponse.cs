namespace InvoiceCheck.Api.Models
{
    public class MockResponse
    {
        public string InvoiceNumber { get; set; } = null!;
        public string TaxNumber { get; set; } = null!;
        public string ResponseCode { get; set; } = null!;
        public string Message { get; set; } = null!;
    }
}