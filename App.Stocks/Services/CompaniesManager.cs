using App.Configuration;
using App.Models.Stocks;
using App.Repositories.Stocks;
using App.Stocks.View;
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
		public CompaniesManager(ICompaniesRepository repository)
		{
			this.repository = repository;
		}

		public IEnumerable<Company> GetAllCompanies()
		{
			return repository.AllCompanies().ToList();
		}

		public IEnumerable<Company> GetCompaniesWithActiveStocks()
		{
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
			var company = repository.CompanyById(id);

			if (company == null)
			{
				return null;
			}

			return company;
		}
	}
}
