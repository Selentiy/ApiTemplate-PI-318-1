using App.Stocks.ModelsView;

namespace App.Stocks.Services
{
	public interface IValidateServices
	{
		void ValidateStocksCompany(Stock stock, Company company);
		void ValidateCompany(Company company, bool v);
		void ValidateDate(string Date);
		void ValidateStock(Stock stock);
	}
}