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

		Task<Restaurant> GetRestaurantByIdAsync(int id);

	}
}
