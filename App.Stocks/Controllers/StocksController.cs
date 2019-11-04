using App.Stocks.Services;
using App.Stocks.View;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

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
		public ActionResult<IEnumerable<StocksListItemView>> CompanyStocks(int id)
		{
			var result = _stocksManager.CompanyStocks(id);
			List<StocksListItemView> stocks = new List<StocksListItemView>();
			foreach(var stock in result)
			{
				stocks.Add(stock.GetStockView());
			}
			return Ok(stocks);
		}

		[HttpGet("companies/{id}/stocks")]
		public ActionResult<StocksListItemView> StockByDate([FromQuery] string Date, int id)
		{
			return Ok(_stocksManager.CompanyStockByDate(id, DateTime.Parse(Date))
				.GetStockView());
		}

		[HttpGet("companies/{id}")]
		public ActionResult<CompanyView> Company(int id)
		{
			var company = _companyManager.GetCompanyById(id);
			if(company == null)
			{
				return NotFound();
			}
			return company.MappSingleCompany();
		}

		[HttpGet("companies/active")]
		public ActionResult<IEnumerable<CompanyView>> CompaniesWithActiveStocks()
		{
			var result = _companyManager.GetCompaniesWithActiveStocks();
			List<CompanyView> companies = new List<CompanyView>();
			foreach (var company in result)
			{
				companies.Add(company.MappSingleCompany());
			}
			return Ok(companies);
		}

		[HttpGet("companies/all")]
		public ActionResult<IEnumerable<CompanyView>> AllCompanies()
		{
			var result = _companyManager.GetAllCompanies();
			List<CompanyView> companies = new List<CompanyView>();
			foreach (var company in result)
			{
				companies.Add(company.MappSingleCompany());
			}
			return Ok(companies);
		}

	}
}
