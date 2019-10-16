using App.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace App.RegularPayments.Controllers
{
    [Route("api/regularpayment/")]
    [ApiController]
    public class RegularPaymentController : ControllerBase
    {
        readonly IPaymentsManager _paymentsManager;
        public RegularPaymentController(IPaymentsManager paymentsManager)
        {
            _paymentsManager = paymentsManager;
        }
        [HttpGet("all")]    
        public ActionResult<IEnumerable<RegularPayment>> GetAllRegPayments()
        {
            var paymentsManagers = _paymentsManager.GetRegularPayments();

            if( paymentsManagers.Count() == 0)
            {
                return NoContent();
            }
            return Ok(paymentsManagers);
        }


    }
}
