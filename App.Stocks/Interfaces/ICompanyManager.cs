using App.Stocks.ModelsView;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace App.Stocks.Interfaces
{
	public interface ICompanyManager
	{
		Task<IEnumerable<CompanyView>> GetCompaniesWithActiveStocksAsync();
		Task<IEnumerable<CompanyView>> GetAllCompaniesAsync();
		Task<CompanyView> GetCompanyByIdAsync(int id);
	}
}
