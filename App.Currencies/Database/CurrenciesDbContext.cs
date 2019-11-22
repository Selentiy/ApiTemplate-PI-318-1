using App.Models.Currencies;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace App.Currencies.Database
{
    public class CurrenciesDbContext : DbContext
    {
        public DbSet<ConversionRate> ConversionRates { get; set; }

        public CurrenciesDbContext(DbContextOptions<CurrenciesDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<ConversionRate>()
                .Property(cr => cr.Currencies)
                .HasConversion(
                d => JsonConvert.SerializeObject(d, Formatting.None),
                s => JsonConvert.DeserializeObject<Dictionary<string, decimal>>(s)
                );
            builder.Entity<ConversionRate>()
                .HasKey(cr => cr.Date);
        }
    }
}
