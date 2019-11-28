using App.Models.Stocks;
using Microsoft.EntityFrameworkCore;

namespace App.Stocks.Database
{
	public class StockDbContext : DbContext
	{
		public DbSet<Company> Companies { get; set; } 
		public DbSet<Stock> Stocks { get; set; }
		public StockDbContext(DbContextOptions<StockDbContext> options) : base(options) { }

		protected override void OnModelCreating(ModelBuilder builder)
		{
			builder.Entity<Company>()
				.HasKey(c => c.OrgId);
			builder.Entity<Company>()
				.HasMany(a => a.Stocks).WithOne(b => b.Company);
			builder.Entity<Stock>()
				.HasKey(s => s.Key);
			builder.Entity<Stock>()
				.HasOne(a => a.Candle).WithOne(b => b.Stock)
				.HasForeignKey<OHCLCandle>(o => o.StockRef);
			builder.Entity<OHCLCandle>()
				.HasKey(o => o.CandleKey);
		}
	}
}
