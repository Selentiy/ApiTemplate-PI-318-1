using App.Configuration;
using App.News.Exceptions;
using App.News.Localization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace App.News.Filters
{
    public class NewsAsyncExceptionFilter : IAsyncExceptionFilter, ITransientDependency
    {
        readonly ILogger<NewsAsyncExceptionFilter> _logger;
        readonly ILocalizationManager _localizationManager;

        public NewsAsyncExceptionFilter(ILogger<NewsAsyncExceptionFilter> logger, ILocalizationManager localizationManager)
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
                        _logger.LogWarning(entityNotFound, $"Method: {entityNotFound.TargetSite}. " +
                            $"Entity Id: {entityNotFound.EntityId}.");
                        context.HttpContext.Response.StatusCode = StatusCodes.Status404NotFound;
                        var errorMessage = _localizationManager.GetResource("EntityNotFoundException");
                        await context.HttpContext.Response.WriteAsync(errorMessage);
                        break;
                    }
                case ArgumentException argumentException:
                    {
                        _logger.LogWarning(argumentException, $"Method: {argumentException.TargetSite}. " +
                            $"Parameter Name: {argumentException.ParamName}.");
                        context.HttpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
                        var errorMessage = _localizationManager.GetResource("ArgumentException");
                        await context.HttpContext.Response.WriteAsync(errorMessage);
                        break;
                    }
                default:
                    {
                        _logger.LogError($"Method: {context.Exception.TargetSite}.");
                        context.HttpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
                        var errorMessage = _localizationManager.GetResource("UnhandeledException");
                        await context.HttpContext.Response.WriteAsync(errorMessage);
                        break;
                    }
            }

            context.ExceptionHandled = true;
        }
    }
}
