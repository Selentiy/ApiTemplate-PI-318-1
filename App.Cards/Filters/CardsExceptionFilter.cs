using App.Cards.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System.Net;
using System.Threading.Tasks;

namespace App.Cards.Filters
{
    public class CardsExceptionFilter : IAsyncExceptionFilter
    {
        readonly string _context;
        readonly ILogger<CardsExceptionFilter> _logger;
        public CardsExceptionFilter(ILogger<CardsExceptionFilter> logger, string context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task OnExceptionAsync(ExceptionContext context)
        {
            _logger.LogError(context.Exception, $"Error occurred in context of {_context}");
            switch (context.Exception)
            {
                case EntityNotFoundException entityNotFound:
                    {
                        context.HttpContext.Response.StatusCode = (int)HttpStatusCode.NotFound;
                        _logger.LogWarning(entityNotFound, entityNotFound.Message + $" Method: {entityNotFound.TargetSite}.");
                        await context.HttpContext.Response.WriteAsync($"Not Found: {entityNotFound.EntityType.Name}");
                        break;
                    }
                case BlockedCardException blockedCard:
                    {
                        context.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                        _logger.LogWarning(blockedCard, blockedCard.Message + $" Method: {blockedCard.TargetSite}.");
                        await context.HttpContext.Response.WriteAsync($"This card by number: {blockedCard.Number} is blocked");
                        break;
                    }
                case InvalidBusinessOperationException invalidBusinessOperation:
                    {
                        context.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                        _logger.LogWarning(invalidBusinessOperation.Message + $" Method: {invalidBusinessOperation.TargetSite}.");
                        await context.HttpContext.Response.WriteAsync("Invalid business operation");
                        break;
                    }
                case PastDateException pastDate:
                    {
                        context.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                        _logger.LogWarning(pastDate.Message + $" Method: {pastDate.TargetSite}.");
                        await context.HttpContext.Response.WriteAsync("Card expiration date is over");
                        break;
                    }
                default:
                    {
                        _logger.LogError(context.Exception.Message + $"Method: {context.Exception.TargetSite}.");
                        context.HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        await context.HttpContext.Response.WriteAsync("Unhandled exception ! Please, contact support for resolve");
                        break;
                    }
            }
            context.ExceptionHandled = true; // this flag should be set to true to stop exception propagation
        }
    }
}
