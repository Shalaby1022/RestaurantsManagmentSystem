using Microsoft.Extensions.Logging;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurants.Application.Restaurants
{
	public class RestaurantService : IRestaurantService
	{
		private readonly IRestaurantRepository _restaurantRepository;
		private readonly ILogger<RestaurantService> _logger;

		public RestaurantService(IRestaurantRepository restaurantRepository,
								ILogger<RestaurantService> logger)
		{
			_restaurantRepository = restaurantRepository ?? throw new ArgumentNullException(nameof(restaurantRepository));
			_logger = logger ?? throw new ArgumentNullException(nameof(logger));
		}

		public async Task<IEnumerable<Restaurant>> GetAllRestaurantAsync()
		{
			var restuarntsRetrieved = await _restaurantRepository.GetAllRestaurantsAsync();
			return restuarntsRetrieved;
		}

		public async Task<Restaurant> GetRestaurantByIdAsync(int id)
		{
			var getASpecificRestaurant = await _restaurantRepository.GetRestaurantByIdAsync(id);
			return getASpecificRestaurant;
		}
	}
}
