
using Microsoft.OpenApi.Writers;
using Restaurants.Application.ServiceExtensions;
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

			// Add services to the container.

			builder.Services.AddControllers()
							.AddJsonOptions(options =>
							{
								options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve;
								options.JsonSerializerOptions.MaxDepth = 64;
							});

			// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
			builder.Services.AddEndpointsApiExplorer();
			builder.Services.AddSwaggerGen();

			#region Infrastructure Service Registration 
			builder.Services.AddInfrastructureExtensionsToRgisterItInMainProgram(builder.Configuration);
			#endregion

			#region Application Service Registration 
			builder.Services.AddApplicationExtensionsToRgisterItInMainProgram(builder.Configuration);
			#endregion


			#region Serilog Registration 
			builder.Host.UseSerilog((context, configuration) =>
			{
				configuration
				.MinimumLevel.Override("Microsoft", Serilog.Events.LogEventLevel.Warning) // remove the redundant noise info retrieved adn override it for more cleaner ones that help 
				.MinimumLevel.Override("Microsoft.EntityFrameWorkCore", Serilog.Events.LogEventLevel.Information)  // capture logs about excuted request 
				.WriteTo.Console(outputTemplate: "[{Timestamp: dd-MM HH:mm:ss} {Level:u3}] |{SourceContext}| {NewLine}{Message:lj}{NewLine}{Exception}") // Override here to make the source where our request got excuted with modifiying Time Stamp with day and month.
				.WriteTo.File("Logs/Restaurant-.log", rollingInterval: RollingInterval.Day, rollOnFileSizeLimit: true);
			}); 



			#endregion


			var app = builder.Build();

			#region SeedingMechanism 
			var scope = app.Services.CreateScope();
			var seeder = scope.ServiceProvider.GetRequiredService<IRestaurantSeeding>();

			 await seeder.SeedAsync();

			#endregion

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

			app.UseAuthorization();


			app.MapControllers();

			app.Run();
		}
	}
}
