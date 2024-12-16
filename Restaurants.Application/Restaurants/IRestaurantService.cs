
using Restaurants.API.Common;
using Restaurants.Application.Parameters;
using Restaurants.Application.Restaurants.DTOS;
using Restaurants.Domain.Entities;
using System.Runtime.CompilerServices;

namespace Restaurants.Application.Restaurants
{
	public interface IRestaurantService
	{
		Task<PagedResult<GetRestaurantDTO>> GetAllRestaurantAsync(ResourceParameters resourceParameters);
		Task<GetRestaurantDTO> GetRestaurantByIdAsync(int id);
        
		Task<CreateRestaurantDto> CreateRestaurantAsync(CreateRestaurantDto dto);
		Task<bool> DeleteRestaurantAsync(int Id);
		Task<UpdateRestaurantDto?> UpdateAnExistingRestaurant(int Id, UpdateRestaurantDto restaurantDto);


	}
}