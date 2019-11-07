using App.News.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Threading.Tasks;

namespace App.News.Filters
{
    public class NewsExceptionFilter : IAsyncExceptionFilter
    {
        readonly string _context;

        public NewsExceptionFilter(string context)
        {
            _context = context;
        }

        public async Task OnExceptionAsync(ExceptionContext context)
        {
            switch (context.Exception)
            {
                case EntityNoContentException entityNoContent:
                    {
                        context.HttpContext.Response.StatusCode = StatusCodes.Status204NoContent;
                        await context.HttpContext.Response.WriteAsync($"No content: {entityNoContent.Message}");
                        break;
                    }
                case EntityNotFoundException entityNotFound:
                    {
                        context.HttpContext.Response.StatusCode = StatusCodes.Status404NotFound;
                        await context.HttpContext.Response.WriteAsync($"Not found: {entityNotFound.Message}");
                        break;
                    }
                case ArgumentException argumentException:
                    {
                        context.HttpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
                        await context.HttpContext.Response.WriteAsync($"Not found: {argumentException.Message}");
                        break;
                    }
                default:
                    {
                        context.HttpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
                        await context.HttpContext.Response.WriteAsync("Unhandled exception! Please, contact support for resolve");
                        break;
                    }
            }

            context.ExceptionHandled = true;
        }
    }
}
