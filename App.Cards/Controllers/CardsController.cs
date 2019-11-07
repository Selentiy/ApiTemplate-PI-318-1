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
            _logger.LogInformation("Call Get method");
            return _cardsManager.GetCard(number, expiresEnd, CVV);
        }

        [HttpPut("block")]
        public ActionResult<bool> BlockCard(long number, DateTime expiresEnd, ushort CVV)
        {
            _logger.LogInformation("Call BlockCard method from");
            _cardsManager.BlockCard(number, expiresEnd, CVV);

            return Ok("Card is blocked!");
        }

        [HttpPut("set/limit")]
        public ActionResult<bool> SetLimit(long number, DateTime expiresEnd, ushort CVV, int limit)
        {
            _logger.LogInformation("Call SetLimit method");
            _cardsManager.SetLimit(number, expiresEnd, CVV, limit);

            return Ok("Limit is set!");

        }

        [HttpPut("remove/limit")]
        public ActionResult<bool> RemoveLimit(long number, DateTime expiresEnd, ushort CVV)
        {
            _logger.LogInformation("Call RemoveLimit method");
            _cardsManager.RemoveLimit(number, expiresEnd, CVV);

            return Ok("Limit is removed!");
        }
    }
}
