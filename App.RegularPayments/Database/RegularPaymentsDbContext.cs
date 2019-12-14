using Microsoft.EntityFrameworkCore;
using App.Models;


namespace App.RegularPayments.Database
{
    public class RegularPaymentsDbContext : DbContext
    {
        public DbSet<RegularPayment> RegularPayments { get; set; }

        public RegularPaymentsDbContext(DbContextOptions<RegularPaymentsDbContext> options) : base(options) { }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<RegularPayment>()
                .HasKey(e => e.PaymentID);
            builder.Entity<RegularPayment>()
                .Property(p => p.PaymentID)
                .ValueGeneratedOnAdd();
        }


    }
}
