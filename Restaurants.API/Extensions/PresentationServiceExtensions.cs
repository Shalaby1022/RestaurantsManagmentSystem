using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Restaurants.API.Middlewares;
using Serilog;

namespace Restaurants.API.Extensions
{
	public static class PresentationServiceExtensions
	{
		public static void AddPresentationExtensionsToRgisterItInMainProgram(this WebApplicationBuilder builder)
		{
			
			#region Exception Handling
			builder.Services.AddScoped<GlobalErrorHandlingMiddleware>();

			#endregion

			#region JsonSerialization Depth Probelem and Circularization
			builder.Services.AddControllers()
							.AddJsonOptions(options =>
							{
								options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve;
								options.JsonSerializerOptions.MaxDepth = 64;
							});

			#endregion

			#region Identity Swagger and Bearer Schema
			builder.Services.AddAuthentication();

			builder.Services.AddEndpointsApiExplorer();
			builder.Services.AddSwaggerGen(c =>
			{
		
				c.AddSecurityDefinition("bearerAuth", new OpenApiSecurityScheme
				{
					Type = SecuritySchemeType.Http,
					Scheme = "Bearer"
				});

				c.AddSecurityRequirement(new OpenApiSecurityRequirement
				{
					{
						new OpenApiSecurityScheme
					{
						Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme , Id = "bearerAuth"}
					},
					[]
				}});

			});
			#endregion

			#region Serilog Registration 
			builder.Host.UseSerilog((context, configuration) =>
			{
				configuration
				.ReadFrom.Configuration(context.Configuration);
			});



			#endregion

		}
	}
}
