using App.Configuration;
using App.Stocks.Interfaces;
using App.Stocks.ModelsView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Stocks.Services
{
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
				Org_Id = company.Org_Id,
				FullName = company.FullName,
				Description = company.Description,
				MainTicker = company.MainTicker
			};

	}
}
