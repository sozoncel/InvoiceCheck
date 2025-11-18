using InvoiceCheck.Api.Models;
using System.Threading;
using System.Threading.Tasks;

namespace InvoiceCheck.Api.Services
{
    public interface IMockProvider
    {
        Task<MockResponse> GetResponseAsync(string invoiceNumber, string taxNumber, CancellationToken ct = default);
    }
}