using Restaurants.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurants.Domain.Interfaces
{
	public interface IRestaurantRepository
	{
		Task<IEnumerable<Restaurant>> GetAllRestaurantsAsync();
		Task<(IEnumerable<Restaurant>, int)> GetAllRestaurantsWithQueryParamsAsync(string? searchQuer , int pageNumber , int pageSize);

		Task<Restaurant> GetRestaurantByIdAsync(int id);

		Task<Restaurant> CreateNewRestaurantAsync(Restaurant restaurant);

		Task<bool> DeleteRestaurantAsync(int Id);

		Task<Restaurant> UpdateRestaurantAsync(Restaurant restaurant);


	}
}
