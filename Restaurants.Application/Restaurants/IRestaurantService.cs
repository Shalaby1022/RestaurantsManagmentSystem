using Restaurants.Application.Restaurants.DTOS;
using Restaurants.Domain.Entities;

namespace Restaurants.Application.Restaurants
{
	public interface IRestaurantService
	{
		Task<IEnumerable<GetRestaurantDTO>> GetAllRestaurantAsync();
		Task<GetRestaurantDTO> GetRestaurantByIdAsync(int id);
        
		Task<CreateRestaurantDto> CreateRestaurantAsync(CreateRestaurantDto dto);

	}
}