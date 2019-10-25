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
		IEnumerable<CompanyView> GetCompaniesWithActiveStocks();
		IEnumerable<CompanyView> GetAllCompanies();
		CompanyView GetCompanyById(int id);
	}

	public class CompaniesManager : ICompanyManager, ITransientDependency
	{
		private ICompaniesRepository repository;
		public CompaniesManager(ICompaniesRepository repository)
		{
			this.repository = repository;
		}

		public IEnumerable<CompanyView> GetAllCompanies()
		{

			var companies = repository.AllCompanies().ToList();

			List<CompanyView> companyViews = new List<CompanyView>();

			foreach (var c in companies)
			{
				companyViews.Add(MappSingleCompany(c));
			}
			return companyViews;
		}

		public IEnumerable<CompanyView> GetCompaniesWithActiveStocks()
		{
			var companies = repository.AllCompanies()
			.Where(comp => comp.Stocks.Any(s => s.IsTraded)).ToList();

			List<CompanyView> companyViews = new List<CompanyView>();

			foreach (var c in companies)
			{
				companyViews.Add(MappSingleCompany(c));
			}
			return companyViews;
		}

		public CompanyView GetCompanyById(int id)
		{
			var company = repository.CompanyById(id);

			if (company == null)
			{
				return null;
			}

			return MappSingleCompany(company);
		}

		private CompanyView MappSingleCompany(Company company) =>
			new CompanyView
			{
				OrgId = company.OrgId,
				FullName = company.FullName,
				Description = company.Description,
				MainTicker = company.MainTicker
			};

	}
}
