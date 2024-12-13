using Restaurants.Application.Dishes.Dtos;
using Restaurants.Application.Restaurants.DTOS;
using Restaurants.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurants.Application.Dishes
{
	public interface IDIshService
	{
		Task<IEnumerable<GetDishDto>> GetAllDishesAsync(int restaurantId);
		Task<GetDishDto> GetDishByIdAsync(int restaurantId, int dishid);
		Task<CreateDishDto> CreateDishAsync(int restaurantId ,  CreateDishDto dto);

		Task<bool> DeleteDishAsync(int restaurantId , int dishId);

	}
}
