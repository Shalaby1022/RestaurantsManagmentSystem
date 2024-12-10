using AutoMapper;
using Microsoft.AspNetCore.Mvc;
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

		public async Task<bool> DeleteRestaurantAsync(int Id)
		{
			if (Id <= 0)
			{
				_logger.LogWarning("Invalid restaurant ID: {Id}", Id);
				throw new ArgumentException("Restaurant ID must be greater than zero.", nameof(Id));
			}

			try
			{
				_logger.LogInformation("Attempting to delete a restaurant with ID: {Id}", Id);

				var restaurantToBeDeleted = await _restaurantRepository.GetRestaurantByIdAsync(Id);
				if (restaurantToBeDeleted == null)
				{
					_logger.LogInformation("No restaurant found with ID: {Id}. Deletion aborted.", Id);
					return false;
				}

				await _restaurantRepository.DeleteRestaurantAsync(Id);

				_logger.LogInformation("Successfully deleted the restaurant with ID: {Id}", Id);
				return true;
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "An error occurred while deleting the restaurant with ID {Id}.", Id);
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

		public async Task<UpdateRestaurantDto?> UpdateAnExistingRestaurant ( int Id , UpdateRestaurantDto restaurantDto)
		{
			if (restaurantDto == null)
			{
				_logger.LogWarning("Attempted to update a null restaurant entity.");
				throw new ArgumentNullException(nameof(restaurantDto), "Restaurant entity cannot be null.");
			}

			try
			{
				var restaurantEntity = await _restaurantRepository.GetRestaurantByIdAsync(Id);

				if (restaurantEntity == null)
				{
					_logger.LogInformation("Restaurant with ID {RestaurantId} not found.", Id);
					return null;
				}

				_mapper.Map(restaurantDto, restaurantEntity);

				var updatedRestaurant = await _restaurantRepository.UpdateRestaurantAsync(restaurantEntity);

				_logger.LogInformation("Restaurant with ID {RestaurantId} successfully updated.", Id);

				return _mapper.Map<UpdateRestaurantDto>(updatedRestaurant);
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "An error occurred while updating the restaurant with ID {RestaurantId}.", Id);
				throw;
			}

		}
	}
}
