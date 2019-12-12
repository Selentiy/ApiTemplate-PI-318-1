using App.Currencies.Exceptions;
using App.Currencies.Localization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System;
using System.Net;
using System.Threading.Tasks;

namespace App.Currencies.Filters
{
    public class CurrenciesExceptionFilter : IAsyncExceptionFilter
    {
        private readonly string _context;
        private readonly ILogger<CurrenciesExceptionFilter> _logger;
        private readonly ILocalizationManager _localizationManager;

        public CurrenciesExceptionFilter(string context, ILogger<CurrenciesExceptionFilter> logger,
            ILocalizationManager localizationManager)
        {
            _context = context;
            _logger = logger;
            _localizationManager = localizationManager;
        }

        public async Task OnExceptionAsync(ExceptionContext context)
        {
            _logger.LogError(context.Exception, $"Error occurred in context of {_context}.");
            switch (context.Exception)
            {
                case EntityNotFoundException entityNotFound:
                    {
                        context.HttpContext.Response.StatusCode = (int)HttpStatusCode.NotFound;
                        await context.HttpContext.Response.WriteAsync(entityNotFound.Message);
                        break;
                    }
                case ArgumentException argument:
                    {
                        context.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                        await context.HttpContext.Response.WriteAsync(argument.Message);
                        break;
                    }
                default:
                    {
                        context.HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        var message = _localizationManager.GetResource("UnhandledException");
                        await context.HttpContext.Response.WriteAsync(message);
                        break;
                    }
            }
            context.ExceptionHandled = true;
        }
    }
}
