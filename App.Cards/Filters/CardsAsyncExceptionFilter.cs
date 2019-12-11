using App.Cards.Exceptions;
using App.Cards.Localization;
using App.Configuration;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System.Net;
using System.Threading.Tasks;

namespace App.Cards.Filters
{
    public class CardsAsyncExceptionFilter : IAsyncExceptionFilter, ITransientDependency
    {
        readonly ILogger<CardsAsyncExceptionFilter> _logger;
        readonly ILocalizationManager _localizationManager;
        public CardsAsyncExceptionFilter(ILogger<CardsAsyncExceptionFilter> logger, ILocalizationManager localizationManager)
        {
            _logger = logger;
            _localizationManager = localizationManager;
        }

        public async Task OnExceptionAsync(ExceptionContext context)
        {
            var _context = context.ActionDescriptor.DisplayName;
            _logger.LogError(context.Exception, $"Error occurred in context of {_context}");
            switch (context.Exception)
            {
                case EntityNotFoundException entityNotFound:
                    {
                        context.HttpContext.Response.StatusCode = (int)HttpStatusCode.NotFound;
                        _logger.LogWarning(entityNotFound, entityNotFound.Message + $" Method: {entityNotFound.TargetSite}.");
                        var errorMessage = _localizationManager.GetResource("EntityNotFoundException");
                        await context.HttpContext.Response.WriteAsync(errorMessage);
                        break;
                    }
                case BlockedCardException blockedCard:
                    {
                        context.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                        _logger.LogWarning(blockedCard, blockedCard.Message + $" Method: {blockedCard.TargetSite}.");
                        var errorMessage = _localizationManager.GetResource("BlockedCardException");
                        await context.HttpContext.Response.WriteAsync(errorMessage);
                        break;
                    }
                case InvalidBusinessOperationException invalidBusinessOperation:
                    {
                        context.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                        _logger.LogWarning(invalidBusinessOperation, invalidBusinessOperation.Message + $" Method: {invalidBusinessOperation.TargetSite}.");
                        var errorMessage = _localizationManager.GetResource("InvalidBusinessOperationException");
                        await context.HttpContext.Response.WriteAsync(errorMessage);
                        break;
                    }
                case PastDateException pastDate:
                    {
                        context.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                        _logger.LogWarning(pastDate, pastDate.Message + $" Method: {pastDate.TargetSite}.");
                        var errorMessage = _localizationManager.GetResource("PastDateException");
                        await context.HttpContext.Response.WriteAsync(errorMessage);
                        break;
                    }
                default:
                    {
                        _logger.LogError(context.Exception.Message + $"Method: {context.Exception.TargetSite}.");
                        context.HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        var errorMessage = _localizationManager.GetResource("UnhandeledException");
                        await context.HttpContext.Response.WriteAsync(errorMessage);
                        break;
                    }
            }
            context.ExceptionHandled = true; // this flag should be set to true to stop exception propagation
        }
    }
}
