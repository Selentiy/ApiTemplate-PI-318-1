using App.Stocks.ModelsView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace App.Stocks.Interfaces
{
	public interface ICompaniesRepository
	{
		IQueryable<Company> AllCompanies();
		Company CompanyById(int id);
	}
}
