using App.Models;
using App.RegularPayments.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using App.RegularPayments.Filters;

namespace App.RegularPayments.Controllers
{
    [Route("api/regularpayment/")]
    [ApiController]
    [TypeFilter(typeof(RegularPaymentsExceptionFilter), Arguments = new object[] { nameof(RegularPaymentController) })]
    public class RegularPaymentController : ControllerBase  
    {
        readonly IPaymentsManager _paymentsManager;
        readonly ILogger<RegularPaymentController> _logger;
        public RegularPaymentController(IPaymentsManager paymentsManager, ILogger<RegularPaymentController> logger)
        {
            _paymentsManager = paymentsManager;
            _logger = logger;
        }

        [HttpGet("all")]    
        public ActionResult<IEnumerable<RegularPayment>> GetAllRegularPayments()
        {
            _logger.LogInformation("GetAllRegularPayments method");
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
            _logger.LogInformation("GetRegularPayment method with id {id}", id);
            var regpay = _paymentsManager.GetRegularPaymentsById(id);
            return regpay;

        }
        [HttpPost]
        public ActionResult CreateRegularPayment([FromBody] RegularPayment regularPayment)
        {
            if (regularPayment == null)
                throw new EntityNotFoundException(typeof(RegularPayment), -1);

            _paymentsManager.AddRegularPaymant(regularPayment);
            return Ok();
        }

        [HttpGet("data")]
        public ActionResult<DateTime> ShowNextPaymentDateByPaymentId(int id)
        {
            var data = _paymentsManager.ShowNextPaymentData(id);

            if (data == null)
                throw new EntityNotFoundException(typeof(RegularPayment), id);

            return Ok(data);
        }
    }
}
