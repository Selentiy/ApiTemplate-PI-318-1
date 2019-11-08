using App.Accounts.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System.Net;
using System.Threading.Tasks;

namespace App.Accounts.Filters
{
    public class AccountsExceptionFilter : IAsyncExceptionFilter
    {
        readonly string _context;
        readonly ILogger<AccountsExceptionFilter> _logger;
        public AccountsExceptionFilter(ILogger<AccountsExceptionFilter> logger, string context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task OnExceptionAsync(ExceptionContext context)
        {
            _logger.LogError(context.Exception, $"Error occurred in context of {_context}.");
            switch (context.Exception)
            {
                case EntityNotFoundException entityNotFound:
                    {
                        _logger.LogWarning(entityNotFound, $"Entity type: {entityNotFound.Message}. Method: {entityNotFound.TargetSite}");
                        context.HttpContext.Response.StatusCode = (int)HttpStatusCode.NotFound;
                        await context.HttpContext.Response.WriteAsync($"Not Found: {entityNotFound.Message}");
                        break;
                    }
                case InvalidBusinessOperationException invalidOperation:
                    {
                        _logger.LogWarning(invalidOperation, $"Method: {invalidOperation.TargetSite}");
                        context.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                        await context.HttpContext.Response.WriteAsync($"Bad request: {invalidOperation.Message}");
                        break;
                    }
                default:
                    {
                        _logger.LogError(context.Exception, $"Method: {context.Exception.TargetSite}.");
                        context.HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        await context.HttpContext.Response.WriteAsync("Unhandled exception! Please, contact support for resolve.");
                        break;
                    }
            }
            context.ExceptionHandled = true;
        }
    }
}
