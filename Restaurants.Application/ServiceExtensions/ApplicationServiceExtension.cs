using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Restaurants.Application.Dishes;
using Restaurants.Application.Restaurants;
using Restaurants.Application.Users;
using Restaurants.Application.Users.Services;


namespace Restaurants.Application.ServiceExtensions
{
	public static class ApplicationServiceExtension
	{
		public static void AddApplicationExtensionsToRgisterItInMainProgram(this IServiceCollection services,
																					IConfiguration configuration)
		{

			services.AddScoped<IRestaurantService, RestaurantService>();
			services.AddScoped<IDIshService , DishService>();

			services.AddAutoMapper(typeof(ApplicationServiceExtension).Assembly);

			services.AddValidatorsFromAssembly(typeof(ApplicationServiceExtension).Assembly)
				.AddFluentValidationAutoValidation();

			services.AddScoped<IUserContext , UserContext>();
			services.AddHttpContextAccessor();

			services.AddScoped<IUserService, UserService>();


		}
	}
}
