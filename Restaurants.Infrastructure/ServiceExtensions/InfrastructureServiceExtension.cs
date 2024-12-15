using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Restaurants.Application;
using Restaurants.Application.Users.EmailSenderUtility;
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

			services.AddAuthorization();

			services.AddIdentity<ApplicationUser, IdentityRole>()
			.AddEntityFrameworkStores<ApplicationDbContext>()
			.AddDefaultTokenProviders()
			.AddSignInManager<SignInManager<ApplicationUser>>();

			//
			services.AddAuthentication()
					.AddBearerToken(IdentityConstants.BearerScheme);

			services.AddAuthorization();

			////

			//services.AddAuthentication(
			//o =>
			//{
			//	o.DefaultAuthenticateScheme = IdentityConstants.ApplicationScheme;
			//	o.DefaultChallengeScheme = IdentityConstants.ApplicationScheme;
			//	o.DefaultSignInScheme = IdentityConstants.ApplicationScheme;
			//})
			//.AddCookie(options =>
			//{
			//	options.LoginPath = "/identity/Unauthorized/";
			//	options.AccessDeniedPath = "/identity/Forbidden/";
			//});

		}
	}
}
