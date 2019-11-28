using App.Stocks.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System.Net;
using System.Threading.Tasks;

namespace App.Stocks
{
	public class StockExceptionFilter : IAsyncExceptionFilter
	{
		readonly ILogger<StockExceptionFilter> logger;
		readonly string context; 

		public StockExceptionFilter(ILogger<StockExceptionFilter> logger, string context)
		{
			this.logger = logger;
			this.context = context;
		}

		public async Task OnExceptionAsync(ExceptionContext context)
		{
			switch (context.Exception)
			{
				case NotFoundException notFound:
					{
						context.HttpContext.Response.StatusCode = (int)HttpStatusCode.NotFound;
						logger.LogWarning(notFound.Message + $"Method: {notFound.TargetSite}.");
					    await context.HttpContext.Response.WriteAsync(notFound.Message);
						break;
					}
				case IncorrectParamException incorrectParam:
					{
						context.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
						logger.LogWarning(incorrectParam.Message + $"Method: {incorrectParam.TargetSite}.");
						await context.HttpContext.Response.WriteAsync(incorrectParam.Message);
						break;
					}
				case PrivateCompanyException privateCompany:
					{
						context.HttpContext.Response.StatusCode = (int)HttpStatusCode.Forbidden;
						logger.LogWarning(privateCompany.Message + $"Method: {privateCompany.TargetSite}.");
						await context.HttpContext.Response.WriteAsync(privateCompany.Message);
						break;
					}
				default:
					{
						logger.LogError(context.Exception.Message + $"Method: {context.Exception.TargetSite}.");
						context.HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
						await context.HttpContext.Response.WriteAsync("Unhandled exception!");
						break;
					}
			}
			context.ExceptionHandled = true;
		}
	}
}
