using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace App.Loans.Controllers
{
     /// <summary>
     /// This is example controller
     /// IMPORTANT the route to your won module should be 'api/{yourModuleName}' in order to avoid conflicts with other modules
     /// </summary>
        [Route("api/example/values")]
        [ApiController]
        public class LoansController : ControllerBase
        {
            // depedencies will be automatically resolved with used DI system
            readonly ISomeService _service;
            readonly IAnotherService _anotherService;
            readonly ILogger<LoansController> _logger;
            readonly IValuesManager _valuesManager;
            public LoansController(
                ISomeService service,
                IAnotherService anotherService,
                ILogger<LoansController> logger,
                IValuesManager valuesManager)
            {
                _service = service;
                _anotherService = anotherService;
                _logger = logger;
                _valuesManager = valuesManager;
            }

            // GET api/example/values
            [HttpGet]
            public ActionResult<IEnumerable<string>> Get()
            {
                _service.DoSmth();
                _anotherService.DoAnything();
                _logger.LogInformation("NOTHING");
                var serviceCallResult = _valuesManager.GetValues().ToList();
                return serviceCallResult;
            }
        }
    }
}
