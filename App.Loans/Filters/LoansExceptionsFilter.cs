using System.Net;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using App.Loans.Exceptions;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace App.Loans.Filters
{
    public class LoansExceptionsFilter : IAsyncExceptionFilter
    {
        readonly string _context;
        readonly ILogger<LoansExceptionsFilter> _logger;
        public LoansExceptionsFilter(ILogger<LoansExceptionsFilter> logger, string context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task OnExceptionAsync(ExceptionContext context)
        {
            _logger.LogError(context.Exception, $"Error occurred in context of {_context}");
            switch (context.Exception)
            {
                case LoanWasClosedException LoanWasClosed:
                    {
                        context.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                        await context.HttpContext.Response.WriteAsync($"Loan was closed! Id {LoanWasClosed.Id}");
                        break;
                    }
                case EntityNotFoundException entityNotFound:
                    {
                        context.HttpContext.Response.StatusCode = (int)HttpStatusCode.NotFound;
                        await context.HttpContext.Response.WriteAsync($"Not Found: {entityNotFound.TypeOfEntity.AssemblyQualifiedName}");
                        break;
                    }
                default:
                    {
                        _logger.LogError($"Method: {context.Exception.TargetSite}.");
                        context.HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        await context.HttpContext.Response.WriteAsync("Unhandled exception ! Please, contact support for resolve");
                        break;
                    }
            }
            context.ExceptionHandled = true;
        }

    }
}

