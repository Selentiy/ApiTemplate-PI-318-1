using App.Currencies.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System.Net;
using System.Threading.Tasks;

namespace App.Currencies.Filters
{
    public class CurrenciesExceptionFilter : IAsyncExceptionFilter
    {
        private readonly string _context;
        private readonly ILogger<CurrenciesExceptionFilter> _logger;

        public CurrenciesExceptionFilter(string context, ILogger<CurrenciesExceptionFilter> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task OnExceptionAsync(ExceptionContext context)
        {
            _logger.LogError(context.Exception, $"Error occurred in context of {_context}.");
            switch (context.Exception)
            {
                case EntityNotFoundException entityNotFound:
                    {
                        context.HttpContext.Response.StatusCode = (int)HttpStatusCode.NotFound;
                        await context.HttpContext.Response.WriteAsync($"Not Found: {entityNotFound.EntityType.AssemblyQualifiedName}.");
                        break;
                    }
                case CurrencyCodeFormatException currencyCodeFormat:
                    {
                        context.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                        await context.HttpContext.Response.WriteAsync(currencyCodeFormat.Message);
                        break;
                    }
                case FutureDateException futureDate:
                    {
                        context.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                        await context.HttpContext.Response.WriteAsync(futureDate.Message);
                        break;
                    }
                default:
                    {
                        context.HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        await context.HttpContext.Response.WriteAsync("Unhandeled exception!");
                        break;
                    }
            }
            context.ExceptionHandled = true;
        }
    }
}
