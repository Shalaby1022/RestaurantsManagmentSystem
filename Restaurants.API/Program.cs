
using Microsoft.OpenApi.Models;
using Microsoft.OpenApi.Writers;
using Restaurants.API.Extensions;
using Restaurants.API.Middlewares;
using Restaurants.Application.ServiceExtensions;
using Restaurants.Domain.Entities;
using Restaurants.Infrastructure.Seeders;
using Restaurants.Infrastructure.ServiceExtensions;
using Serilog;

namespace Restaurants.API
{
	public class Program
	{
		public static async Task Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);


			#region API Service Registration 

			builder.AddPresentationExtensionsToRgisterItInMainProgram();

			#endregion

			#region Infrastructure Service Registration 

			builder.Services.AddInfrastructureExtensionsToRgisterItInMainProgram(builder.Configuration);

			#endregion

			#region Application Service Registration 

			builder.Services.AddApplicationExtensionsToRgisterItInMainProgram(builder.Configuration);

			#endregion


			var app = builder.Build();

			#region SeedingMechanism 
			var scope = app.Services.CreateScope();
			var seeder = scope.ServiceProvider.GetRequiredService<IRestaurantSeeding>();

			 await seeder.SeedAsync();

			#endregion

			app.UseMiddleware<GlobalErrorHandlingMiddleware>();

			#region Serilog 
			app.UseSerilogRequestLogging();
			#endregion

			// Configure the HTTP request pipeline.
			if (app.Environment.IsDevelopment())
			{
				app.UseSwagger();
				app.UseSwaggerUI();
			}

			app.UseHttpsRedirection();

			app.MapGroup("/identity")
		   .WithTags("Identity") 
		   .MapIdentityApi<ApplicationUser>();


			app.MapIdentityApi<ApplicationUser>();
			app.UseAuthentication();
			app.UseAuthorization();


			app.MapControllers();

			app.Run();
		}
	}
}
