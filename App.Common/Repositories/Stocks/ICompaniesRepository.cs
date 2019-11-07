using App.Models.Stocks;
using System.Linq;

namespace App.Repositories.Stocks
{
	public interface ICompaniesRepository
	{
		IQueryable<Company> AllCompanies();
		Company CompanyById(int id);
	}
}
