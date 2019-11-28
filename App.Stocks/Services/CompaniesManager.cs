using App.Configuration;
using App.Models.Stocks;
using App.Repositories.Stocks;
using App.Stocks.Exceptions;
using App.Stocks.View;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;

namespace App.Stocks.Services
{
	public interface ICompanyManager
	{
		IEnumerable<Company> GetCompaniesWithActiveStocks();
		IEnumerable<Company> GetAllCompanies();
		Company GetCompanyById(int id);
	}

	public class CompaniesManager : ICompanyManager, ITransientDependency
	{
		private ICompaniesRepository repository;
		private ILogger<CompaniesManager> logger;
		public CompaniesManager(ICompaniesRepository repository, ILogger<CompaniesManager> logger)
		{
			this.repository = repository;
			this.logger = logger;
		}

		public IEnumerable<Company> GetAllCompanies()
		{
			logger.LogInformation("Call GetAllCompanies method");
			return repository.AllCompanies().ToList();
		}

		public IEnumerable<Company> GetCompaniesWithActiveStocks()
		{
			logger.LogInformation("Call GetCompaniesWithActiveStocks method");
			var companies = repository.AllCompanies()
			.Where(comp => comp.Stocks.Any(s => s.IsTraded)).ToList();

			List<Company> company = new List<Company>();

			foreach (var c in companies)
			{
				company.Add(c);
			}
			return company;
		}

		public Company GetCompanyById(int id)
		{
			logger.LogInformation("Call GetCompanyById method");
			var company = repository.CompanyById(id);

			if (company == null)
			{
				return null;
			}

			return company;
		}
	}
}
