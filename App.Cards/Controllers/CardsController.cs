using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using App.Cards.Interfaces;
using App.Models.Cards;
using App.Cards.Filters;

namespace App.Cards.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [TypeFilter(typeof(CardsExceptionFilter), Arguments = new object[] { nameof(CardsController) })]
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
        public ActionResult<bool> BlockCard(long number, DateTime expiresEnd, ushort CVV)
        {
            _cardsManager.BlockCard(number, expiresEnd, CVV);

            return Ok("Card is blocked!");
        }

        [HttpPut("set/limit")]
        public ActionResult<bool> SetLimit(long number, DateTime expiresEnd, ushort CVV, int limit)
        {
            _cardsManager.SetLimit(number, expiresEnd, CVV, limit);

            return Ok("Limit is set!");

        }

        [HttpPut("remove/limit")]
        public ActionResult<bool> RemoveLimit(long number, DateTime expiresEnd, ushort CVV)
        {
            _cardsManager.RemoveLimit(number, expiresEnd, CVV);

            return Ok("Limit is removed!");
        }
    }
}
