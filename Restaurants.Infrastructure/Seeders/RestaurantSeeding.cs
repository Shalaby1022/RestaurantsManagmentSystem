using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.VisualBasic;
using Restaurants.Application;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurants.Infrastructure.Seeders
{
	public class RestaurantSeeding : IRestaurantSeeding
	{
		private readonly IApplicationDbContext _dbContext;
		private readonly ILogger<RestaurantSeeding> _logger;

		public RestaurantSeeding(IApplicationDbContext dbContext,
									ILogger<RestaurantSeeding> logger)
		{
			_dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
			_logger = logger ?? throw new ArgumentNullException(nameof(logger));
		}
		public async Task SeedAsync()
		{
			try
			{
				if (_dbContext.Restaurants == null)
				{
					_logger.LogError("_dbContext.Restaurants is null.");
					throw new InvalidOperationException("_dbContext.Restaurants is null.");
				}
				if (!_dbContext.Restaurants.Any())
				{
					var restaurants = GetRestaurants();
					await _dbContext.Restaurants.AddRangeAsync(restaurants);
					await _dbContext.SaveChangesAsync();
					_logger.LogInformation("Successfully seeded the Restaurants database.");
				}
				else
				{
					_logger.LogInformation("The Restaurants database is already seeded.");
				}

			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Error while seeding the Restaurants database.");
			}
		}


		private IEnumerable<IdentityRole> GetRoles()
		{
			List<IdentityRole> roles =
				[
				  new (Domain.Utilities.IdentityConstants.Admin),
				  new (Domain.Utilities.IdentityConstants.Owner),
				  new (Domain.Utilities.IdentityConstants.User)
				];

			return roles;
		}

			
		private IEnumerable<Restaurant> GetRestaurants()
		{
			return new List<Restaurant>
			{
				new Restaurant
				{
					Name = "Gourmet Paradise",
					Description = "Fine dining experience with international cuisines.",
					Category = "Fine Dining",
					HasDeleivery = true,
					ContanctEmail = "info@gourmetparadise.com",
					ContactNumber = "123-456-7890",
					Address = new Address
					{
						Street = "123 Main St",
						City = "Metropolis",
						Country = "Fictionland",
						PostalCode = "12345",
						PhoneNumber = "123-456-7890"
					},
					Dishes = new List<Dish>
					{
						new Dish
						{
							Name = "Grilled Salmon",
							Description = "Served with a lemon butter sauce.",
							KiloCalories = 350,
							Price = 25.99m
						},
						new Dish
						{
							Name = "Truffle Pasta",
							Description = "Pasta with creamy truffle sauce and parmesan.",
							KiloCalories = 450,
							Price = 22.50m
						}
					}
				},
				new Restaurant
				{
					Name = "Pasta Hub",
					Description = "Your favorite Italian dishes under one roof.",
					Category = "Casual Dining",
					HasDeleivery = true,
					ContanctEmail = "contact@pastahub.com",
					ContactNumber = "987-654-3210",
					Address = new Address
					{
						Street = "456 Elm St",
						City = "Gotham",
						Country = "Fictionland",
						PostalCode = "54321",
						PhoneNumber = "987-654-3210"
					},
					Dishes = new List<Dish>
					{
						new Dish
						{
							Name = "Spaghetti Bolognese",
							Description = "Classic spaghetti with rich meat sauce.",
							KiloCalories = 400,
							Price = 14.99m
						},
						new Dish
						{
							Name = "Margherita Pizza",
							Description = "Fresh tomato, mozzarella, and basil.",
							KiloCalories = 300,
							Price = 12.00m
						}
					}
				}
			};
		}
	}
}
