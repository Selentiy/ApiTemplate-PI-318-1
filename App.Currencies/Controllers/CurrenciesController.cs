using System;
using System.Collections.Generic;
using System.Linq;
using App.Currencies.Filters;
using App.Currencies.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace App.Currencies.Controllers
{
    [Route("api/currencies")]
    [ApiController]
    [TypeFilter(typeof(CurrenciesExceptionFilter), Arguments = new object[] { nameof(CurrenciesController)})]
    public class CurrenciesController : ControllerBase
    {
        private readonly ILogger<CurrenciesController> _logger;
        private readonly ICurrencyManager _currencyManager;

        public CurrenciesController(
            ILogger<CurrenciesController> logger,
            ICurrencyManager currencyManager)
        {
            _logger = logger;
            _currencyManager = currencyManager;
        }

        [HttpGet]
        public ActionResult<IEnumerable<string>> GetCurrencyCodes()
        {
            _logger.LogDebug("call GetCurrencyCodes method");
            var serviceCallResult = _currencyManager.GetCurrencyCodes().ToList();
            return serviceCallResult;
        }

        [HttpGet("{code}/{date}")]
        public ActionResult<IEnumerable<KeyValuePair<string, decimal>>> GetRate(string code, DateTime date)
        {
            _logger.LogDebug("call GetRate method");
            if (String.IsNullOrEmpty(code))
                return BadRequest();
            var serviceCallResult = _currencyManager.GetExchangeRate(code.ToUpper(), date)
                .ToDictionary(x => x.Key, x => x.Value);
            return serviceCallResult;
        }
    }
}
