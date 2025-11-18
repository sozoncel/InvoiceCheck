using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using InvoiceCheck.Api.Models;
using InvoiceCheck.Api.Services;
using InvoiceCheck.Api.Data;
using InvoiceCheck.Api.Entities;
using System.Threading.Tasks;
using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace InvoiceCheck.Api.Controllers
{
    [ApiController]
    [Route("api/invoice")]
    public class InvoiceController : ControllerBase
    {
        private readonly IMockProvider _mock;
        private readonly IMemoryCache _cache;
        private readonly ApplicationDbContext _db;

        public InvoiceController(IMockProvider mock, IMemoryCache cache, ApplicationDbContext db)
        {
            _mock = mock;
            _cache = cache;
            _db = db;
        }

        [HttpPost("check")]
        public async Task<IActionResult> Check([FromBody] InvoiceCheckRequest req)
        {
            var correlationId = Guid.NewGuid().ToString();
            Console.WriteLine($"[{correlationId}] Request: Invoice={req.InvoiceNumber} Tax={req.TaxNumber}");

            string key = $"{req.TaxNumber}-{req.InvoiceNumber}";

            if (!_cache.TryGetValue(key, out MockResponse mockResp))
            {
                mockResp = await _mock.GetResponseAsync(req.InvoiceNumber, req.TaxNumber);
                _cache.Set(key, mockResp, TimeSpan.FromMinutes(1));
            }

            Console.WriteLine($"[{correlationId}] Mock Response: {mockResp.ResponseCode} - {mockResp.Message}");

            var last = await _db.InvoiceStatusLogs
                .Where(x => x.InvoiceNumber == req.InvoiceNumber && x.TaxNumber == req.TaxNumber)
                .OrderByDescending(x => x.Id)
                .FirstOrDefaultAsync();

            bool blocked = last != null &&
                           last.ResponseCode == "REJECTED" &&
                           mockResp.ResponseCode == "REJECTED";

            var log = new InvoiceStatusLog
            {
                InvoiceNumber = req.InvoiceNumber,
                TaxNumber = req.TaxNumber,
                ResponseCode = mockResp.ResponseCode,
                ResponseMessage = mockResp.Message,
                RequestTime = DateTime.UtcNow
            };

            _db.InvoiceStatusLogs.Add(log);
            await _db.SaveChangesAsync();
            Console.WriteLine($"[{correlationId}] DB Log Saved. ID: {log.Id}");

            if (blocked)
            {
                return Ok(new
                {
                    status = "BLOCKED",
                    message = "Bu faturaya ait art arda 2 red cevabý alýndý. Manuel inceleme gerekiyor."
                });
            }

            return Ok(new { status = mockResp.ResponseCode, message = mockResp.Message });
        }
    }
}