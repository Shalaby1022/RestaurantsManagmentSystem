using Restaurants.Domain.Entities;

namespace Restaurants.Application.Restaurants
{
	public interface IRestaurantService
	{
		Task<IEnumerable<Restaurant>> GetAllRestaurantAsync();
		Task<Restaurant> GetRestaurantByIdAsync(int id);

	}
}