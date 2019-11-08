using App.News.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace App.News.Filters
{
    public class NewsExceptionFilter : IAsyncExceptionFilter
    {
        readonly string _context;
        readonly ILogger<NewsExceptionFilter> _logger;

        public NewsExceptionFilter(ILogger<NewsExceptionFilter> logger, string context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task OnExceptionAsync(ExceptionContext context)
        {
            _logger.LogError(context.Exception, $"Error occurred in context of {_context}");
            switch (context.Exception)
            {
                case EntityNoContentException entityNoContent:
                    {
                        _logger.LogWarning(entityNoContent, $"Method: {entityNoContent.TargetSite}. " +
                            $"Entity Type: {entityNoContent.EntityType.Name}.");
                        context.HttpContext.Response.StatusCode = StatusCodes.Status204NoContent;
                        _logger.LogWarning(entityNoContent, $"");
                        await context.HttpContext.Response.WriteAsync($"No content: {entityNoContent.Message}");
                        break;
                    }
                case EntityNotFoundException entityNotFound:
                    {
                        _logger.LogWarning(entityNotFound, $"Method: {entityNotFound.TargetSite}. " +
                            $"Entity Id: {entityNotFound.EntityId}.");
                        context.HttpContext.Response.StatusCode = StatusCodes.Status404NotFound;
                        await context.HttpContext.Response.WriteAsync($"Not found: {entityNotFound.Message}");
                        break;
                    }
                case ArgumentException argumentException:
                    {
                        _logger.LogWarning(argumentException, $"Method: {argumentException.TargetSite}. " +
                            $"Parameter Name: {argumentException.ParamName}.");
                        context.HttpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
                        await context.HttpContext.Response.WriteAsync($"Bad request: {argumentException.Message}");
                        break;
                    }
                default:
                    {
                        _logger.LogError($"Method: {context.Exception.TargetSite}.");
                        context.HttpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
                        await context.HttpContext.Response.WriteAsync("Unhandled exception! Please, contact support for resolve.");
                        break;
                    }
            }

            context.ExceptionHandled = true;
        }
    }
}
