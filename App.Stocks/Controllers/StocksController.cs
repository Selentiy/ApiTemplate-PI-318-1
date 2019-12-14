﻿using App.Models.Stocks;
using App.Stocks.Exceptions;
using App.Stocks.Services;
using App.Stocks.View;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;

namespace App.Stocks.Controllers
{
	[Route("api/stocks")]
	[ApiController]
	[ServiceFilter(typeof(StockExceptionFilter))]
	public class StocksController : ControllerBase
	{
		readonly IStocksManager _stocksManager;
		readonly ICompanyManager _companyManager;
		readonly ILogger<StocksController> _logger;

		public StocksController(IStocksManager stocksManager,
			ICompanyManager companyManager, ILogger<StocksController> logger)
		{
			_stocksManager = stocksManager;
			_companyManager = companyManager;
			_logger = logger;
		}

		[HttpGet("companies/{id}/stocks/all")]
		public ActionResult<IEnumerable<StocksListItemView>> CompanyStocks(int id)
		{
			_logger.LogInformation($"Call CompanyStocks method with id : {id}");
			var result = _stocksManager.CompanyStocks(id);
			if (result == null)
			{
				throw new PrivateCompanyException(id);
			}
			List<StocksListItemView> stocks = new List<StocksListItemView>();
			foreach(var stock in result)
			{
				stocks.Add(stock.GetStockView());
			}
			return stocks;
		}

		[HttpGet("companies/{id}/stocks")]
		public ActionResult<StocksListItemView> StockByDate([FromQuery] string Date, int id)
		{
			_logger.LogInformation($"Call StockByDate with id : {id} , date: {Date}");
			if (!DateTime.TryParse(Date, out var date))
			{
				throw new IncorrectDateException();
			}
			return _stocksManager.CompanyStockByDate(id, date)
			.GetStockView();
		}

		[HttpGet("companies/{id}")]
		public ActionResult<CompanyView> Company(int id)
		{
			_logger.LogInformation($"Call Company(by id) with id : {id}");
			var company = _companyManager.GetCompanyById(id);
			if(company == null)
			{
				throw new NotFoundException(typeof(Company), id);
			}
			return company.MappSingleCompany();
		}

		[HttpGet("companies/active")]
		public ActionResult<IEnumerable<CompanyView>> CompaniesWithActiveStocks()
		{
			_logger.LogInformation($"Call CompaniesWithActiveStocks");
			var result = _companyManager.GetCompaniesWithActiveStocks();
			List<CompanyView> companies = new List<CompanyView>();
			foreach (var company in result)
			{
				companies.Add(company.MappSingleCompany());
			}
			return companies;
		}

		[HttpGet("companies/all")]
		public ActionResult<IEnumerable<CompanyView>> AllCompanies()
		{
			_logger.LogInformation($"Call AllCompanies");
			var result = _companyManager.GetAllCompanies();
			List<CompanyView> companies = new List<CompanyView>();
			foreach (var company in result)
			{
				companies.Add(company.MappSingleCompany());
			}
			return companies;
		}

	}
}
