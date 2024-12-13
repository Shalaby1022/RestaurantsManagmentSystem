using AutoMapper;
using Microsoft.Extensions.Logging;
using Restaurants.Application.Dishes.Dtos;
using Restaurants.Application.Exceptions;
using Restaurants.Application.Restaurants;
using Restaurants.Application.Restaurants.DTOS;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurants.Application.Dishes
{
	public class DishService : IDIshService
	{
		private readonly IDishRepository _dishRepository;
		private readonly IRestaurantRepository _restaurantRepository;
		private readonly ILogger<DishService> _logger;
		private readonly IMapper _mapper;

		public DishService(IDishRepository dishRepository,
								IRestaurantRepository restaurantRepository,
								ILogger<DishService> logger,
								IMapper mapper)
		{
			_dishRepository = dishRepository ?? throw new ArgumentNullException(nameof(dishRepository));
			_restaurantRepository = restaurantRepository ?? throw new ArgumentNullException(nameof(restaurantRepository));
			_logger = logger ?? throw new ArgumentNullException(nameof(logger));
			_mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
		}
		public async Task<CreateDishDto> CreateDishAsync(int restaurantId, CreateDishDto dto)
		{
			var restaurantExists = await _restaurantRepository.GetRestaurantByIdAsync(restaurantId);
			if (restaurantExists == null)
			{
				_logger.LogWarning("Attempted to retireve a null restaurant entity.");
				throw new NotFoundException(nameof(Restaurant), restaurantId.ToString());
			}
			try
			{
				_logger.LogInformation("Attempting to create a new dish: {@DishName}", dto.Name);

				var dishEntity = _mapper.Map<Dish>(dto);
				dishEntity.RestaurantId = restaurantId;

				var createdDish = await  _dishRepository.CreateNewDishAsync(dishEntity);

				var createdDto = _mapper.Map<CreateDishDto>(createdDish);

				_logger.LogInformation("Restaurant {RestaurantName} successfully created with ID {RestaurantId}.",
					createdDish.Name, createdDish.Id);

				return createdDto;
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "An error occurred while creating a new restaurant.");
				throw new InternalServerErrorException("Internal Server Error!");
			}

		}

		public async Task<bool> DeleteDishAsync(int restaurantId, int dishId)
		{
			var restaurantExists = await _restaurantRepository.GetRestaurantByIdAsync(restaurantId);
			if (restaurantExists == null)
			{
				_logger.LogWarning("Attempted to retireve a null restaurant entity.");
				throw new NotFoundException(nameof(Restaurant), restaurantId.ToString());
			}

			if (dishId <= 0)
			{
				_logger.LogWarning("Invalid restaurant ID: {@Id}", dishId);
				throw new NotFoundException(nameof(Dish), dishId.ToString());
			}

			var dishToBeDeleted = await _dishRepository.GetDishByIdAsync(dishId);
			if (dishToBeDeleted == null)
			{
				_logger.LogInformation("No restaurant found with ID: {@Id}. Deletion aborted.", dishId);
				throw new NotFoundException(nameof(Dish), dishId.ToString());

			}
			dishToBeDeleted.Id = restaurantId;

			await _restaurantRepository.DeleteRestaurantAsync(dishId);

			_logger.LogInformation("Successfully deleted the restaurant with ID: {@Id}", dishId);
			return true;
		}

		public async Task<IEnumerable<GetDishDto>> GetAllDishesAsync(int restaurantId)
		{
			var restaurantExists = await _restaurantRepository.GetRestaurantByIdAsync(restaurantId);
			if (restaurantExists == null)
			{
				_logger.LogWarning("Attempted to retireve a null restaurant entity.");
				throw new NotFoundException(nameof(Restaurant), restaurantId.ToString());
			}

			var dishToBeRetrieved = await _dishRepository.GetAllDishesAsync();
			var dishesTOBeMappedToDto = _mapper.Map<IEnumerable<GetDishDto>>(dishToBeRetrieved);

			foreach (var dish in dishesTOBeMappedToDto)
			{
				dish.Id = restaurantId;  
			}

			return dishesTOBeMappedToDto;
		}

		public async Task<GetDishDto> GetDishByIdAsync(int restaurantId , int dishid)
		{
			var restaurantExists = await _restaurantRepository.GetRestaurantByIdAsync(restaurantId);
			if (restaurantExists == null)
			{
				_logger.LogWarning("Attempted to retireve a null restaurant entity.");
				throw new NotFoundException(nameof(Restaurant), restaurantId.ToString());
			}

			var getASpecificDish= await _dishRepository.GetDishByIdAsync(dishid);

			if (getASpecificDish == null)
				throw new NotFoundException(nameof(getASpecificDish), dishid.ToString());


			if (getASpecificDish == null)
				throw new NotFoundException("Dish", dishid.ToString());

			var dishToBeMappedToDto = _mapper.Map<GetDishDto>(getASpecificDish);
			dishToBeMappedToDto.Id = restaurantId;

			return dishToBeMappedToDto;
		}
	}
}
