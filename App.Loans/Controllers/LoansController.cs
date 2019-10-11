using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using App.Loans.Interface;

namespace App.Loans.Controllers
{

    [Route("api/loans")]
    [ApiController]
    public class LoansController : ControllerBase
    {
        readonly IAnotherService _anotherService;
        readonly ILoanManger _loansManager;
        public LoansController(
            IAnotherService anotherService,
            ILoanManger loansManager)
        {
            _anotherService = anotherService;
            _loansManager = loansManager;
        }

        // GET api/loans/values
        [HttpGet("values")]
        public ActionResult<IEnumerable<string>> GetActiveLoans()
        {
            _anotherService.DoAnything();
            var serviceCallResult = _loansManager.GetValues().ToList();
            return serviceCallResult;
        }

        // GET api/loans/{Id}
        [HttpGet("{Id}")]
        public ActionResult<IEnumerable<string>> GetAmountOfPaymentsLeft(int Id)
        {
            _anotherService.DoAnything();
            var serviceCallResult = _loansManager.AmountOfPaymentsLeft(Id).ToList();
            return serviceCallResult;
        }
    }

}
