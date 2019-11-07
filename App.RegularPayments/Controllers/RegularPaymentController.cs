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
        public ActionResult<IEnumerable<RegularPayment>> GetAllRegularPayments()
        {
            var regpayments = _paymentsManager.GetRegularPayments();

            if(regpayments.Count() == 0)
            {
                return NoContent();
            }
            return Ok(regpayments);
        }

        [HttpGet("{id}")]
        public ActionResult<RegularPayment> GetRegularPayment(int id)
        {
            var regpay = _paymentsManager.GetRegularPaymentsById(id);
            if (regpay == null)
               return  NotFound();
            return regpay;

        }
        [HttpPost]
        public ActionResult CreateRegularPayment([FromBody] RegularPayment regularPayment)
        {
            bool result = _paymentsManager.AddRegularPaymant(regularPayment);
            if (!result)
                return BadRequest();

            return Ok();
        }

        [HttpGet("data")]
        public ActionResult<DateTime> ShowNextPaymentDateByPaymentId(int id)
        {
            var data = _paymentsManager.ShowNextPaymentData(id);

            if (data == null)
                return NoContent();

            return Ok(data);
        }
    }
}
