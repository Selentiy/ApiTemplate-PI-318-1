using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace App.Currencies.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CurrenciesController : ControllerBase
    {
        readonly ILogger<CurrenciesController> _logger;
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
            var serviceCallResult = _currencyManager.GetCurrencyCodes()?.ToList();
            if (serviceCallResult == null || serviceCallResult.Count == 0)
                return NotFound(serviceCallResult);
            return serviceCallResult;
        }

        [Route("{code}/{date}")]
        [HttpGet]
        public ActionResult<IEnumerable<KeyValuePair<string, decimal>>> GetRate(string code, DateTime date)
        {
            var serviceCallResult = _currencyManager.GetExchangeRate(code.ToUpper(), date)
                ?.ToDictionary(x => x.Key, x => x.Value);
            if (serviceCallResult == null || serviceCallResult.Count == 0)
                return NotFound(serviceCallResult);
            return serviceCallResult;
        }
    }
}
