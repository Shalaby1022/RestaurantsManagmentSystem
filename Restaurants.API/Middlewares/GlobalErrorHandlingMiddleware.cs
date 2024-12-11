using Microsoft.AspNetCore.Http.HttpResults;
using Restaurants.Application.Exceptions;
using System.Net;
using System.Text.Json;
using NotImplementedException = Restaurants.Application.Exceptions.NotImplementedException;
using UnauthorizedAccessException = Restaurants.Application.Exceptions.UnauthorizedAccessException;


namespace Restaurants.API.Middlewares
{
	public class GlobalErrorHandlingMiddleware : IMiddleware
	{
		private readonly ILogger<GlobalErrorHandlingMiddleware> _logger;

		public GlobalErrorHandlingMiddleware(ILogger<GlobalErrorHandlingMiddleware> logger)
        {
			_logger = logger;
		}
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
		{
			try
			{
				await next.Invoke(context);
			}
			catch(NotFoundException notFoundException)
			{
				context.Response.StatusCode = 404;
				await context.Response.WriteAsync(notFoundException.Message);

				_logger.LogWarning(notFoundException.Message);
			}
			catch (Exception ex)
			{
				_logger.LogError(ex , ex.Message);

				context.Response.StatusCode = 500;
				await context.Response.WriteAsync("Internal Server Error");
			}
		}
	}
}
