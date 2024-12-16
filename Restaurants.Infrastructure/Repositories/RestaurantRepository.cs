using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Restaurants.Application;
using Restaurants.Application.Exceptions;
using Restaurants.Application.Parameters;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
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

		public async Task<Restaurant> CreateNewRestaurantAsync(Restaurant restaurant)
		{
			if (restaurant == null) throw new ArgumentNullException(nameof(restaurant));

			await _dbContext.Restaurants.AddAsync(restaurant);
			await _dbContext.SaveChangesAsync();
			return restaurant;
		}

		public async Task<bool> DeleteRestaurantAsync(int Id)
		{
			var restaurant = await _dbContext.Restaurants.FirstOrDefaultAsync(x => x.Id == Id);

			if (restaurant == null) return false;
			
			_dbContext.Restaurants.Remove(restaurant);
			await _dbContext.SaveChangesAsync();

			return true;
		}

		public async Task<IEnumerable<Restaurant>> GetAllRestaurantsAsync()
		{
			try
			{
				var restaurantsFromDb = await _dbContext.Restaurants
														.Include(d=>d.Dishes)										
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

		public async Task<(IEnumerable<Restaurant> , int)> GetAllRestaurantsWithQueryParamsAsync(string? searchQuery , 
																									int pageNumber ,
																									int pageSize ,
																									string? sortBy , 
																									SortDirection sortDirection)
		{
			try
			{
				// Filtering
				var searchQueryTrimmed = searchQuery?.ToLower();
				var baseQuery = _dbContext.Restaurants
						.Where(r => string.IsNullOrEmpty(searchQuery) ||
									r.Name.ToLower().Contains(searchQueryTrimmed) ||
									r.Description.ToLower().Contains(searchQueryTrimmed));

				// Get Total Counts For pagination purpose
				var totalCount =  baseQuery.Count();

				// Sorting 
				if (sortBy != null)
				{
					var coloumnsSelected = new Dictionary<string, Expression<Func<Restaurant, object>>>
					{
						{nameof(Restaurant.Name) , r => r.Name },
						{nameof(Restaurant.Description) , r => r.Description },
						{nameof(Restaurant.Category) , r=> r.Category}
					};

					var selectedColoumns = coloumnsSelected[sortBy];
					baseQuery = sortDirection == SortDirection.Ascending
						? baseQuery.OrderBy(selectedColoumns)
						: baseQuery.OrderByDescending(selectedColoumns);
				}

				// pagination 
				var restaurantsFromDb = await baseQuery
						.Skip(pageSize * (pageNumber -1))
						.Take(pageSize)
						.ToListAsync();


				if (restaurantsFromDb == null || !restaurantsFromDb.Any())
				{
					_logger.LogInformation("No restaurants found in the database.");
				}

				_logger.LogInformation($"Retrieved {restaurantsFromDb.Count} restaurants from the database.");
				return (restaurantsFromDb , totalCount);
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
					_logger.LogWarning("Invalid restaurant ID: {@Id}", id);
					    throw new NotFoundException("Restaurant" , id.ToString());
				}

				var restaurant = await _dbContext.Restaurants
												  .Include (d=>d.Dishes)
												  .FirstOrDefaultAsync(r => r.Id == id);


				if (restaurant == null)
				{
					_logger.LogInformation("Restaurant not found with ID: {@Id}", id);
					     throw new NotFoundException(nameof(restaurant) , id.ToString());
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

		public async Task<Restaurant> UpdateRestaurantAsync(Restaurant restaurant)
		{
			if (restaurant == null) throw new ArgumentNullException(nameof(restaurant));

			 _dbContext.Restaurants.Update(restaurant);
			 await _dbContext.SaveChangesAsync();

			 return restaurant;
		}
	}
	}
