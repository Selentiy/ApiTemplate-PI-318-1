using System;
using System.Collections.Generic;
using System.Linq;
using App.Example.Dto;
using App.Example.Filters;
using App.Example.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace App.Example.Controllers
{
    /// <summary>
    /// This is example controller
    /// IMPORTANT the route to your won module should be 'api/{yourModuleName}' in order to avoid conflicts with other modules
    /// </summary>
    [Route("api/example/values")]
    [ApiController]
    [ServiceFilter(typeof(ExampleAsyncExceptionFilter))]
    public class ValuesController : ControllerBase
    {
        // depedencies will be automatically resolved with used DI system
        readonly ILogger<ValuesController> _logger;
        readonly IValuesManager _valuesManager;
        public ValuesController(
            ILogger<ValuesController> logger,
            IValuesManager valuesManager)
        {
            _logger = logger;
            _valuesManager = valuesManager;
        }

        // GET api/example/values
        [HttpGet]
        public IEnumerable<SimpleValueDto> Get()
        {
            _logger.LogDebug("call Get method");
            var serviceCallResult = _valuesManager.GetValues().ToList();
            return serviceCallResult.Select(x => new SimpleValueDto(x)).ToList();
        }

        // GET api/example/values/{key}
        [HttpGet("{key}")]
        public SimpleValueDto GetByKey(string key)
        {
            _logger.LogDebug("call GetByKey method");
            var value = _valuesManager.GetValueByKey(key);
            return new SimpleValueDto(value);
        }

        // POST api/example/values/operation
        [HttpPost("operation")]
        public void PerformOperation()
        {
            _logger.LogDebug("call PerformOperation method");
            _valuesManager.PerformBusinessOperation();
        }

        // POST api/example/values/exception
        [HttpPost("exception")]
        public void CauseException()
        {
            _logger.LogDebug("call CauseException method");
            throw new Exception("Some hidden information");
        }
    }
}