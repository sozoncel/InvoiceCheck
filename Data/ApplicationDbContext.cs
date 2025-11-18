using InvoiceCheck.Api.Entities;
using Microsoft.EntityFrameworkCore;

namespace InvoiceCheck.Api.Data
{
    public class ApplicationDbContext : Microsoft.EntityFrameworkCore.DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> opts) : base(opts) { }
        public Microsoft.EntityFrameworkCore.DbSet<InvoiceStatusLog> InvoiceStatusLogs { get; set; } = null!;
    }
}