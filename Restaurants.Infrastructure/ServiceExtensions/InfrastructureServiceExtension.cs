﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Restaurants.Application;
using Restaurants.Infrastructure.Persistance.Data;
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
					options.UseSqlServer(connectionString));

			services.AddScoped<IApplicationDbContext, ApplicationDbContext>();

			services.AddScoped<IRestaurantSeeding, RestaurantSeeding>();
		}
	}
}
