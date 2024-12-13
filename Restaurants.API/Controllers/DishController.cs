using Microsoft.AspNetCore.Mvc;
using Restaurants.Application.Dishes;
using Restaurants.Application.Dishes.Dtos;
using Restaurants.Application.Exceptions;
using Restaurants.Application.Restaurants;
using Restaurants.Application.Restaurants.DTOS;

namespace Restaurants.API.Controllers
{
	[ApiController]
	[Route("restaurant/{restaurantId}/dish")]
	[Produces("application/json", "application/xml")]
	[Consumes("application/json", "application/xml")]
	public class DishController : ControllerBase
	{
		private readonly IDIshService _dIshService;
		private readonly ILogger<RestaurantController> _logger;

		public DishController(      IDIshService dIshService,
									ILogger<RestaurantController> logger)
		{
			_dIshService = dIshService ?? throw new ArgumentNullException(nameof(dIshService));
			_logger = logger ?? throw new ArgumentNullException(nameof(logger));
		}

		//[HttpGet(Name = nameof(GetAllDishes))]
		//[ProducesResponseType(StatusCodes.Status200OK)]
		//[ProducesResponseType(StatusCodes.Status406NotAcceptable)]
		//[ProducesResponseType(StatusCodes.Status415UnsupportedMediaType)]
		//[ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
		//[ProducesResponseType(StatusCodes.Status500InternalServerError)]
		//public async Task<IActionResult> GetAllDishes()
		//{


		//}

		[HttpGet(Name = nameof(GetAllDishes))]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status406NotAcceptable)]
		[ProducesResponseType(StatusCodes.Status415UnsupportedMediaType)]
		[ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]
		public async Task<IActionResult> GetAllDishes(int restaurantId)
		{

			try
			{
				var diheList = await _dIshService.GetAllDishesAsync(restaurantId);

				return Ok(diheList);

			}
			catch (Exception ex)
			{
				_logger.LogError(ex, $"An Error Occurred While Retreiveing The Comments Info {nameof(GetAllDishes)}");
				return StatusCode(500);
			}
		}

		[HttpGet("{dishId:int}", Name = nameof(GetDishById))]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status406NotAcceptable)]
		[ProducesResponseType(StatusCodes.Status415UnsupportedMediaType)]
		[ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]
		public async Task<IActionResult> GetDishById(int restaurantId , [FromRoute] int dishId)
		{
			var dish = await _dIshService.GetDishByIdAsync(restaurantId , dishId);

			if (dish == null)
			{
				throw new NotFoundException(nameof(dish) , dishId.ToString());
			}

			return Ok(dish);
		}

		[HttpPost(Name = nameof(CreateDish))]
		[ProducesResponseType(StatusCodes.Status201Created)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status406NotAcceptable)]
		[ProducesResponseType(StatusCodes.Status415UnsupportedMediaType)]
		[ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]

		public async Task<IActionResult> CreateDish([FromRoute] int restaurantId , [FromBody] CreateDishDto dishDto)
		{
			if (!ModelState.IsValid)
			{
				_logger.LogError($"Invalid POST attempt in {nameof(CreateDish)}");
				return BadRequest(ModelState);
			}
			try
			{
				var CreatedDish = await _dIshService.CreateDishAsync(restaurantId, dishDto);
				if (CreatedDish == null)
					throw new NotFoundException(nameof(dishDto), restaurantId.ToString());

				return StatusCode(201, CreatedDish);
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, $"An error occurred while creating and adding new Dish {nameof(CreateDish)}.");
				throw new InternalServerErrorException("Internal Server Error!");
			}
		}

		[HttpDelete("{dishId:int}", Name = nameof(DeleteDish))]
		[ProducesResponseType(StatusCodes.Status204NoContent)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status406NotAcceptable)]
		[ProducesResponseType(StatusCodes.Status415UnsupportedMediaType)]
		[ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]
		public async Task<IActionResult> DeleteDish(int restaurantId , [FromRoute]int dishId)
		{
			try
			{
				var isDeleted = await _dIshService.DeleteDishAsync(restaurantId , dishId);

				if (!isDeleted)
				{
					_logger.LogInformation("Dish with ID {dishId} not found.", dishId);
					return NotFound($"DIsh with ID {dishId} not found.");
				}

				_logger.LogInformation("Dish with ID {dishId} successfully deleted.", dishId);
				return StatusCode(204 , "Deleted Successfully");
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "An error occurred while deleting the dish with ID {dishId}.", dishId);
				return StatusCode(StatusCodes.Status500InternalServerError, "An unexpected error occurred.");
			}

		}

	}
}
