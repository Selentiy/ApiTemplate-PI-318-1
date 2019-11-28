using App.Models.Stocks;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

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
				.HasMany(a => a.Stocks).WithOne(b => b.Company)
				.HasPrincipalKey(c => c.OrgId);
			builder.Entity<Stock>()
				.HasOne(a => a.Candle).WithOne(b => b.Stock)
				.HasForeignKey<OHCLCandle>(o => o.StockRef)
				.HasPrincipalKey<Stock>(s => s.Key);
		}
	}
}
