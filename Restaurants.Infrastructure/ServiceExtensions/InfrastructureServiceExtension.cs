using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Restaurants.Application;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Interfaces;
using Restaurants.Infrastructure.Persistance.Data;
using Restaurants.Infrastructure.Repositories;
using Restaurants.Infrastructure.Seeders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurants.Infrastructure.ServiceExtensions
{
	public static class InfrastructureServiceExtension
	{
		public static void AddInfrastructureExtensionsToRgisterItInMainProgram(this IServiceCollection services,
																					IConfiguration configuration)
		{
			var connectionString = configuration.GetConnectionString("DefaultConnection");

			services.AddDbContext<ApplicationDbContext>(options =>
					options.UseSqlServer(connectionString)
					// instead of returning passed pararmeters as like encode ---@_id__ i want actual value pased so i enable this.
					 .EnableSensitiveDataLogging());

			services.AddScoped<IApplicationDbContext, ApplicationDbContext>();

			services.AddScoped<IRestaurantSeeding, RestaurantSeeding>();

			services.AddScoped<IRestaurantRepository , RestaurantRepository>();

			services.AddScoped<IDishRepository , DishRepository>();


			// identity 
			services.AddIdentityApiEndpoints<ApplicationUser>()
				.AddEntityFrameworkStores<ApplicationDbContext>();
		}
	}
}
