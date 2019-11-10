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
        readonly ILoanManger _loansManager;
        public LoansController(
            ILoanManger loansManager)
        {
            _loansManager = loansManager;
        }

        // GET api/loans/values
        [HttpGet("values/listactiveloans")]
        public ActionResult<IEnumerable<string>> GetListActiveLoans()
        {
            var serviceCallResult = _loansManager.GetValuesInStringArray().ToList();
            return serviceCallResult;
        }

        // GET api/loans/{Id}
        [HttpGet("{Id}/paymentsleft")]
        public ActionResult<IEnumerable<string>> GetAmountOfPaymentsLeft(int Id)
        {
            var serviceCallResult = _loansManager.AmountOfPaymentsLeft(Id).ToList();
            return serviceCallResult;
        }
    }

}
