using Restaurants.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurants.Domain.Interfaces
{
	public interface IDishRepository
	{
		Task<IEnumerable<Dish>> GetAllDishesAsync();
		Task<Dish> GetDishByIdAsync(int id);
		Task<Dish> CreateNewDishAsync(Dish dish);

		Task<bool> DeleteRestaurantAsync(int Id);


	}
}
