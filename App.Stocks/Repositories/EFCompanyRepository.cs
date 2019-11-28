using App.Configuration;
using App.Models.Stocks;
using App.Repositories.Stocks;
using App.Stocks.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace App.Stocks.Repositories
{
	public class EFCompanyRepository : ICompaniesRepository, ITransientDependency, IDisposable
	{
		private readonly StockDbContext _dbContext;

		public EFCompanyRepository(StockDbContext dbContext)
		{
			_dbContext = dbContext;
		}
		 
		public Company CompanyById(int id)
		{ 
			return _dbContext.Companies.Where(c => c.OrgId == id).FirstOrDefault();
		}

		public IQueryable<Company> AllCompanies()
		{
			var values = _dbContext.Companies;
			return values.AsQueryable();
		}

		public void Dispose()
		{
			_dbContext?.Dispose();
		}
	}
}
