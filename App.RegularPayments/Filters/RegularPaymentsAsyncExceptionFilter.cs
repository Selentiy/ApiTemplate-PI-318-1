using App.Configuration;
using App.RegularPayments.Exceptions;
using App.RegularPayments.Localization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace App.RegularPayments.Filters
{
    public class RegularPaymentsAsyncExceptionFilter : IAsyncExceptionFilter, ITransientDependency
    {
        readonly ILogger<RegularPaymentsAsyncExceptionFilter> _logger;
        readonly ILocalizationManager _localizationManager;
        public RegularPaymentsAsyncExceptionFilter(ILogger<RegularPaymentsAsyncExceptionFilter> logger, ILocalizationManager localizationManager)
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
                case EntityNullException entityNull:
                    {
                        _logger.LogWarning(entityNull, $"Method: {entityNull.TargetSite}. ");
                        context.HttpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
                        var errorMessage = _localizationManager.GetResource("EntityIsNullException");
                        await context.HttpContext.Response.WriteAsync(errorMessage);
                        break;
                    }
                case ArgumentException argumentException:
                    {
                        _logger.LogWarning(argumentException, $"Method: {argumentException.TargetSite}. " +
                            $"Parameter Name: {argumentException.ParamName}.");
                        context.HttpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
                        var errorMessage = _localizationManager.GetResource("InvalidOperationException");
                        await context.HttpContext.Response.WriteAsync(errorMessage);
                        break;
                    }
                default:
                    {
                        _logger.LogError($"Method: {context.Exception.TargetSite}.");
                        context.HttpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
                        var errorMessage = _localizationManager.GetResource("UnhandledException");
                        await context.HttpContext.Response.WriteAsync(errorMessage);
                        break;

                    }

            }
            context.ExceptionHandled = true;
        }
        
    }
}
