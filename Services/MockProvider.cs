using InvoiceCheck.Api.Models;
using Microsoft.Extensions.Hosting;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace InvoiceCheck.Api.Services
{
    public class MockProvider : IMockProvider
    {
        private readonly List<MockResponse> _items;

        public MockProvider(IHostEnvironment env)
        {
            var path = Path.Combine(env.ContentRootPath, "mock-responses.json");
            var txt = File.ReadAllText(path);
            _items = JsonSerializer.Deserialize<List<MockResponse>>(txt, new JsonSerializerOptions { PropertyNameCaseInsensitive = true })!;
        }

        public Task<MockResponse> GetResponseAsync(string invoiceNumber, string taxNumber, CancellationToken ct = default)
        {
            var match = _items.FirstOrDefault(i =>
                i.InvoiceNumber == invoiceNumber &&
                i.TaxNumber == taxNumber);

            return Task.FromResult(match ?? new MockResponse {
                InvoiceNumber = invoiceNumber,
                TaxNumber = taxNumber,
                ResponseCode = "APPROVED",
                Message = "Varsayılan mock"
            });
        }
    }
}