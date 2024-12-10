using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Restaurants.Application;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurants.Infrastructure.Repositories
{
	public class RestaurantRepository : IRestaurantRepository
	{
		private readonly IApplicationDbContext _dbContext;
		private readonly ILogger<RestaurantRepository> _logger;

		public RestaurantRepository(IApplicationDbContext dbContext ,
									ILogger<RestaurantRepository> logger)
        {
			_dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
			_logger = logger ?? throw new ArgumentNullException(nameof(logger));	
		}

		public async Task<IEnumerable<Restaurant>> GetAllRestaurantsAsync()
		{
			try
			{
				var restaurantsFromDb = await _dbContext.Restaurants
														.ToListAsync();

				if (restaurantsFromDb == null || !restaurantsFromDb.Any())
				{
					_logger.LogInformation("No restaurants found in the database.");
					return Enumerable.Empty<Restaurant>();
				}

				_logger.LogInformation($"Retrieved {restaurantsFromDb.Count} restaurants from the database.");
				return restaurantsFromDb;
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "An error occurred while retrieving restaurants from the database.");
				throw;
			}
		}

		public async Task<Restaurant> GetRestaurantByIdAsync(int id)
		{
			try
			{

				if (id <= 0)
				{
					_logger.LogWarning("Invalid restaurant ID: {Id}", id);
					return null;
				}

				var restaurant = await _dbContext.Restaurants
												  .FirstOrDefaultAsync(r => r.Id == id);


				if (restaurant == null)
				{
					_logger.LogInformation("Restaurant not found with ID: {Id}", id);
					return null;
				}

				_logger.LogInformation($"Restaurant found: {restaurant.Name}");
				return restaurant;
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "An error occurred while retrieving the restaurant by ID.");
				throw;
			}
		}

		}
	}
