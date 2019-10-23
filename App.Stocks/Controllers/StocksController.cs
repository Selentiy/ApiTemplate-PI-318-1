using App.Stocks.Services;
using App.Stocks.View;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace App.Stocks.Controllers
{
	[Route("api/stocks")]
	[ApiController]
	public class StocksController : ControllerBase
	{
		readonly IStocksManager _stocksManager;
		readonly ICompanyManager _companyManager;

		public StocksController(IStocksManager stocksManager,
			ICompanyManager companyManager)
		{
			_stocksManager = stocksManager;
			_companyManager = companyManager;
		}

		[HttpGet("companies/{id}/stocks/all")]
		public async Task<IEnumerable<StocksListItemView>> CompanyStocks(int id)
		{
			return await _stocksManager.CompanyStocksAsync(id);
		}

		[HttpGet("companies/{id}/stocks")]
		public async Task<StocksListItemView> StockByDate([FromQuery] string Date, int id)
		{
			return await _stocksManager.CompanyStockByDate(id, DateTime.Parse(Date));
		}

		[HttpGet("companies/{id}")]
		public async Task<CompanyView> Company(int id)
		{
			var company = await _companyManager.GetCompanyByIdAsync(id);
			if(company == null)
			{
				throw new HttpListenerException((int)HttpStatusCode.NotFound, "Company not found");
			}
			return company;
		}

		[HttpGet("companies/active")]
		public async Task<IEnumerable<CompanyView>> CompaniesWithActiveStocks()
		{
			return await _companyManager.GetCompaniesWithActiveStocksAsync();
		}

		[HttpGet("companies/all")]
		public async Task<IEnumerable<CompanyView>> AllCompanies()
		{
			return await _companyManager.GetAllCompaniesAsync();
		}
	}
}
