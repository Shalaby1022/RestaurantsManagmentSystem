
using Microsoft.OpenApi.Writers;
using Restaurants.Infrastructure.Seeders;
using Restaurants.Infrastructure.ServiceExtensions;

namespace Restaurants.API
{
	public class Program
	{
		public static async Task Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			// Add services to the container.

			builder.Services.AddControllers();
			// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
			builder.Services.AddEndpointsApiExplorer();
			builder.Services.AddSwaggerGen();

			#region Infrastructure Service Registration 
			builder.Services.AddInfrastructureExtensionsToRgisterItInMainProgram(builder.Configuration);
			#endregion



			var app = builder.Build();

			#region SeedingMechanism 
			var scope = app.Services.CreateScope();
			var seeder = scope.ServiceProvider.GetRequiredService<IRestaurantSeeding>();

			 await seeder.SeedAsync();

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
