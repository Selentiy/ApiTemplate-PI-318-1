using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using App.Cards.Interfaces;
using App.Models.Cards.Models;

namespace App.Cards.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CardsController : ControllerBase
    {
        private readonly ILogger<CardsController> _logger;
        private readonly ICardsManager _cardsManager;

        public CardsController(
            ILogger<CardsController> logger,
            ICardsManager cardsManager)
        {
            _logger = logger;
            _cardsManager = cardsManager;
        }

        
        [HttpGet("get")]
        public ActionResult<Card> Get(long number, DateTime expiresEnd, ushort CVV)
        {
            var serviceCallResult = _cardsManager.GetCard(number, expiresEnd, CVV);

            if (serviceCallResult == null)
                return NotFound();
            return serviceCallResult;
        }

        [HttpPut("block")]
        public ActionResult<bool> BlockCard(string ownerName, long number, DateTime expiresEnd, ushort CVV)
        {
            var serviceCallResult = _cardsManager.BlockCard(ownerName, number, expiresEnd, CVV);

            if (serviceCallResult)
                return Ok("Card is blocked!");
            return serviceCallResult;
        }

        [HttpPut("set/limit")]
        public ActionResult<bool> SetLimit(long number, DateTime expiresEnd, ushort CVV, int limit)
        {
            var serviceCallResult = _cardsManager.SetLimit(number, expiresEnd, CVV, limit);

            if (serviceCallResult)
                return Ok("Limit is set!");
            return serviceCallResult;
        }

        [HttpPut("remove/limit")]
        public ActionResult<bool> RemoveLimit(long number, DateTime expiresEnd, ushort CVV)
        {
            var serviceCallResult = _cardsManager.RemoveLimit(number, expiresEnd, CVV);

            if (serviceCallResult)
                return Ok("Limit is removed!");
            return serviceCallResult;
        }
    }
}
