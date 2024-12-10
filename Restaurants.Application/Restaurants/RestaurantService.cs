using AutoMapper;
using Microsoft.Extensions.Logging;
using Restaurants.Application.Restaurants.DTOS;
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
		private readonly IMapper _mapper;

		public RestaurantService(IRestaurantRepository restaurantRepository,
								ILogger<RestaurantService> logger,
								IMapper mapper)
		{
			_restaurantRepository = restaurantRepository ?? throw new ArgumentNullException(nameof(restaurantRepository));
			_logger = logger ?? throw new ArgumentNullException(nameof(logger));
			_mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
		}

		public async Task<CreateRestaurantDto> CreateRestaurantAsync(CreateRestaurantDto restaurantDto)
		{
			if (restaurantDto == null)
			{
				_logger.LogWarning("Attempted to create a null restaurant entity.");
				throw new ArgumentNullException(nameof(restaurantDto), "Restaurant entity cannot be null.");
			}

			try
			{
				_logger.LogInformation("Attempting to create a new restaurant: {RestaurantName}", restaurantDto.Name);

				var restaurantEntity = _mapper.Map<Restaurant>(restaurantDto);

				var createdRestaurant = await _restaurantRepository.CreateNewRestaurantAsync(restaurantEntity);

				var createdDto = _mapper.Map<CreateRestaurantDto>(createdRestaurant);

				_logger.LogInformation("Restaurant {RestaurantName} successfully created with ID {RestaurantId}.",
					createdRestaurant.Name, createdRestaurant.Id);

				return createdDto;
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "An error occurred while creating a new restaurant.");
				throw;
			}
		}
	

		public async Task<IEnumerable<GetRestaurantDTO>> GetAllRestaurantAsync()
		{
			var restuarntsRetrieved = await _restaurantRepository.GetAllRestaurantsAsync();

			var restarauntsTOBeMappedToDto =  _mapper.Map<IEnumerable<GetRestaurantDTO>>(restuarntsRetrieved);

			return restarauntsTOBeMappedToDto;
		}

		public async Task<GetRestaurantDTO> GetRestaurantByIdAsync(int id)
		{
			var getASpecificRestaurant = await _restaurantRepository.GetRestaurantByIdAsync(id);

		   var restaurantToBeMappedToDto =  _mapper.Map<GetRestaurantDTO>(getASpecificRestaurant);

			return restaurantToBeMappedToDto;
		}
	}
}
