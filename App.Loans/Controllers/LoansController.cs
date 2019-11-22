using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using App.Loans.Interface;
using Microsoft.Extensions.Logging;
using App.Loans.Models;
using App.Loans.Exceptions;
using App.Loans.Filters;

namespace App.Loans.Controllers
{

    [Route("api/loans")]
    [ApiController]
    [TypeFilter(typeof(LoansExceptionsFilter), Arguments = new object[] { nameof(LoansController) })]
    public class LoansController : ControllerBase
    {
        readonly ILoanManger _loansManager;
        readonly ILogger<LoansController> _logger;

        public LoansController(
            ILoanManger loansManager,
            ILogger<LoansController> logger)
        {
            _loansManager = loansManager;
            _logger = logger;
        }

        // GET api/loans/values
        [HttpGet("values/listactiveloans")]
        public ActionResult<IEnumerable<Loan>> GetListActiveLoans()
        {
            _logger.LogInformation("Call GetListActiveLoans Method");
            var serviceCallResult = _loansManager.GetLoans().ToList();
            return serviceCallResult;
        }

        // GET api/loans/{Id}
        [HttpGet("{Id}/paymentsleft")]
        public ActionResult<string> GetAmountOfPaymentsLeft(int Id)
        {
            _logger.LogInformation($"Call GetAmountOfPaymentsLeft Method with Id = {Id}");
            var serviceCallResult = _loansManager.AmountOfPaymentsLeft(Id);
            return serviceCallResult;
        }
    }

}
