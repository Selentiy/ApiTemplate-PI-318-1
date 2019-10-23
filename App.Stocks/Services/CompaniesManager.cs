using App.Configuration;
using App.Models.Stocks;
using App.Repositories.Stocks;
using App.Stocks.View;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App.Stocks.Services
{
	public interface ICompanyManager
	{
		Task<IEnumerable<CompanyView>> GetCompaniesWithActiveStocksAsync();
		Task<IEnumerable<CompanyView>> GetAllCompaniesAsync();
		Task<CompanyView> GetCompanyByIdAsync(int id);
	}

	public class CompaniesManager : ICompanyManager, ITransientDependency
	{
		private ICompaniesRepository repository;
		public CompaniesManager(ICompaniesRepository repository)
		{
			this.repository = repository;
		}

		public async Task<IEnumerable<CompanyView>> GetAllCompaniesAsync()
		{

			var companies = await Task.Run(() => repository.AllCompanies().ToList());

			List<CompanyView> companyViews = new List<CompanyView>();

			foreach (var c in companies)
			{
				companyViews.Add(MappSingleCompany(c));
			}
			return companyViews;
		}

		public async Task<IEnumerable<CompanyView>> GetCompaniesWithActiveStocksAsync()
		{
			var companies = await Task.Run(() => repository.AllCompanies()
			.Where(comp => comp.Stocks.Any(s => s.IsTraded)).ToList());

			List<CompanyView> companyViews = new List<CompanyView>();

			foreach (var c in companies)
			{
				companyViews.Add(MappSingleCompany(c));
			}
			return companyViews;
		}

		public async Task<CompanyView> GetCompanyByIdAsync(int id)
		{
			var company = await Task.Run(() => repository.CompanyById(id));

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
