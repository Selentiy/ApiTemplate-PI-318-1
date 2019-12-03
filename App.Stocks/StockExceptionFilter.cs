using App.Configuration;
using App.Stocks.Exceptions;
using App.Stocks.Localization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System.Net;
using System.Threading.Tasks;

namespace App.Stocks
{
	public class StockExceptionFilter : IAsyncExceptionFilter, ITransientDependency
	{
		readonly ILogger<StockExceptionFilter> logger;
		readonly ILocalizationManager _localizationManager;

		public StockExceptionFilter(ILogger<StockExceptionFilter> logger, 
			ILocalizationManager localizationManager)
		{
			this.logger = logger;
			_localizationManager = localizationManager;
		}

		public async Task OnExceptionAsync(ExceptionContext context)
		{
			var _context = context.ActionDescriptor.DisplayName;
			logger.LogError(context.Exception, $"Error occurred in context of {_context}");
			switch (context.Exception)
			{
				case NotFoundException notFound:
					{
						context.HttpContext.Response.StatusCode = (int)HttpStatusCode.NotFound;
						var errorMessage = _localizationManager.GetResource("ResourceNotFound");
					    await context.HttpContext.Response.WriteAsync(errorMessage);
						break;
					}
				case IncorrectParamException incorrectParam:
					{
						context.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
						var errorMessage = _localizationManager.GetResource(incorrectParam.Message);
						await context.HttpContext.Response.WriteAsync(errorMessage);
						break;
					}
				case PrivateCompanyException privateCompany:
					{
						context.HttpContext.Response.StatusCode = (int)HttpStatusCode.Forbidden;
						var errorMessage = _localizationManager.GetResource("PrivateCompany");
						await context.HttpContext.Response.WriteAsync(errorMessage);
						break;
					}
				default:
					{
						var errorMessage = _localizationManager.GetResource(context.Exception.Message);
						context.HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
						await context.HttpContext.Response.WriteAsync("Unhandled exception!");
						break;
					}
			}
			context.ExceptionHandled = true;
		}
	}
}
