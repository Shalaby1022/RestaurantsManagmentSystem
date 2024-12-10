using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Restaurants.Application.Restaurants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurants.Application.ServiceExtensions
{
	public static class ApplicationServiceExtension
	{
		public static void AddApplicationExtensionsToRgisterItInMainProgram(this IServiceCollection services,
																					IConfiguration configuration)
		{

			services.AddScoped<IRestaurantService, RestaurantService>();

		}
	}
}
