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
			return Ok(_stocksManager.CompanyStocks(id));
		}

		[HttpGet("companies/{id}/stocks")]
		public ActionResult<StocksListItemView> StockByDate([FromQuery] string Date, int id)
		{
			return Ok(_stocksManager.CompanyStockByDate(id, DateTime.Parse(Date)));
		}

		[HttpGet("companies/{id}")]
		public ActionResult<CompanyView> Company(int id)
		{
			var company = _companyManager.GetCompanyById(id);
			if(company == null)
			{
				return BadRequest();
			}
			return company;
		}

		[HttpGet("companies/active")]
		public ActionResult<IEnumerable<CompanyView>> CompaniesWithActiveStocks()
		{
			return Ok(_companyManager.GetCompaniesWithActiveStocks());
		}

		[HttpGet("companies/all")]
		public ActionResult<IEnumerable<CompanyView>> AllCompanies()
		{
			return Ok(_companyManager.GetAllCompanies());
		}
	}
}
