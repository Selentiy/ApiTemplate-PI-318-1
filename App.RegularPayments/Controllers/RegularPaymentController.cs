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
        readonly ICreateManager _createManager;
        readonly IShowNextServise _showNextServise;
        public RegularPaymentController(IPaymentsManager paymentsManager, ICreateManager createManager, IShowNextServise showNextServise)
        {
            _paymentsManager = paymentsManager;
            _createManager = createManager;
            _showNextServise = showNextServise;
        }
        [HttpGet("(all)")]    
        public ActionResult<IEnumerable<RegularPayment>> GetAllRegPayments()
        {
            var paymentsManagers = _paymentsManager.GetRegularPayments();

            if( paymentsManagers.Count() == 0)
            {
                return NoContent();
            }
            return Ok(paymentsManagers);
        }

        [HttpGet("{id}")]
        public ActionResult<RegularPayment> GetRegPay(int id)
        {
            var regpay = _paymentsManager.GetRegularPaymentsById(id);
            if (regpay == null)
               return  NotFound();
            return regpay;

        }

        [HttpPost("(add)")]
        public ActionResult CreateRegPayments([FromBody] RegularPayment regularPayment)
        {
            bool result = _createManager.AddRegularPaymant(regularPayment);
            if (!result)
                return BadRequest();

            return Ok();
        }

        [HttpGet("data")]
        public ActionResult<DateTime> GetDay(int id)
        {
            var data = _showNextServise.ShowNextData(id);

            if (data == null)
                return NoContent();

            return Ok(data);
        }
    }
}
