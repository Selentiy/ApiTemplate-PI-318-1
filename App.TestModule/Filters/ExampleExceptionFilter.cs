using System.Net;
using System.Threading.Tasks;
using App.Configuration;
using App.Example.Exceptions;
using App.Example.Localization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;

namespace App.Example.Filters
{
    /// <summary>
    /// Example of async exception filter.
    /// The purpose of this class -> catch unhandeled exceptions and wrap sensitive data into common form
    /// </summary>
    public class ExampleAsyncExceptionFilter : IAsyncExceptionFilter, ITransientDependency
    {
        readonly ILogger<ExampleAsyncExceptionFilter> _logger;
        readonly ILocalizationManager _localizationManager;
        public ExampleAsyncExceptionFilter(
            ILogger<ExampleAsyncExceptionFilter> logger,
            ILocalizationManager localizationManager)
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
                        var errorMessage = _localizationManager.GetResource("ResourceNotFound");
                        await context.HttpContext.Response.WriteAsync(errorMessage);
                        break;
                    }
                case InvalidBusinessOperationException invalidBusinessOperationException:
                    {
                        context.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                        var errorMessage = _localizationManager.GetResource(invalidBusinessOperationException.Message);
                        await context.HttpContext.Response.WriteAsync(errorMessage);
                        break;
                    }
                default:
                    {
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