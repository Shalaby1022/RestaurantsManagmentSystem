using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Restaurants.Application.Restaurants;
using Restaurants.Application.Restaurants.DTOS;
using Restaurants.Domain.Entities;

namespace Restaurants.API.Controllers
{
	[ApiController]
	[Route("restaurant")]
	[Produces("application/json", "application/xml")]
	[Consumes("application/json", "application/xml")]
	[Authorize]

	public class RestaurantController : ControllerBase
	{
		private readonly IRestaurantService _restaurantService;
		private readonly ILogger<RestaurantController> _logger;

		public RestaurantController(IRestaurantService restaurantService,
									ILogger<RestaurantController> logger)
		{
			_restaurantService = restaurantService ?? throw new ArgumentNullException(nameof(restaurantService));
			_logger = logger ?? throw new ArgumentNullException(nameof(logger));
		}

		[HttpGet(Name = nameof(GetAllRestaurants))]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status406NotAcceptable)]
		[ProducesResponseType(StatusCodes.Status415UnsupportedMediaType)]
		[ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]
		[AllowAnonymous]
		public async Task<IActionResult> GetAllRestaurants()
		{

			try
			{
				var restaurantsList = await _restaurantService.GetAllRestaurantAsync();

				return Ok(restaurantsList);

			}
			catch (Exception ex)
			{
				_logger.LogError(ex, $"An Error Occurred While Retreiveing The Comments Info {nameof(GetAllRestaurants)}");
				return StatusCode(500);
			}
		}

		[HttpGet("{restaurantId:int}", Name = nameof(GetRestaurantById))]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status406NotAcceptable)]
		[ProducesResponseType(StatusCodes.Status415UnsupportedMediaType)]
		[ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]
		public async Task<IActionResult> GetRestaurantById([FromRoute] int restaurantId)
		{
			var restaurant = await _restaurantService.GetRestaurantByIdAsync(restaurantId);

			if (restaurant == null)
			{
				return NotFound();
			}

			return Ok(restaurant);
		}

		[HttpPost(Name = nameof(CreateRestaurant))]
		[ProducesResponseType(StatusCodes.Status201Created)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status406NotAcceptable)]
		[ProducesResponseType(StatusCodes.Status415UnsupportedMediaType)]
		[ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]

		public async Task<IActionResult> CreateRestaurant([FromBody] CreateRestaurantDto restaurantDto)
		{
			if (!ModelState.IsValid)
			{
				_logger.LogError($"Invalid POST attempt in {nameof(CreateRestaurant)}");
				return BadRequest(ModelState);
			}
			try
			{
				if (restaurantDto == null) return BadRequest(ModelState);

				var createdRestauratns = await _restaurantService.CreateRestaurantAsync(restaurantDto);

				return StatusCode(201, createdRestauratns);
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, $"An error occurred while creating and adding new Restaurant {nameof(CreateRestaurant)}.");
				return StatusCode(500, "Internal server error");
			}
		}

		[HttpDelete("{restaurantId:int}", Name = nameof(DeleteRestaurant))]
		[ProducesResponseType(StatusCodes.Status204NoContent)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status406NotAcceptable)]
		[ProducesResponseType(StatusCodes.Status415UnsupportedMediaType)]
		[ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]
		public async Task<IActionResult> DeleteRestaurant([FromRoute] int restaurantId)
		{
			try
			{
				var isDeleted = await _restaurantService.DeleteRestaurantAsync(restaurantId);

				if (!isDeleted)
				{
					_logger.LogInformation("Restaurant with ID {RestaurantId} not found.", restaurantId);
					return NotFound($"Restaurant with ID {restaurantId} not found.");
				}

				_logger.LogInformation("Restaurant with ID {RestaurantId} successfully deleted.", restaurantId);
				return NoContent();
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "An error occurred while deleting the restaurant with ID {RestaurantId}.", restaurantId);
				return StatusCode(StatusCodes.Status500InternalServerError, "An unexpected error occurred.");
			}

		}

		[HttpPut("{resturantId:long}", Name = nameof(UpdateRestaurant))]
		[ProducesResponseType(StatusCodes.Status204NoContent)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status406NotAcceptable)]
		[ProducesResponseType(StatusCodes.Status415UnsupportedMediaType)]
		[ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]

		public async Task<IActionResult> UpdateRestaurant(int resturantId , UpdateRestaurantDto restaurantDto)
		{
			if (!ModelState.IsValid)
			{
				_logger.LogError($"Invalid Update attempt in {nameof(UpdateRestaurant)}");
				return BadRequest(ModelState);
			}

			try
			{
				if (restaurantDto == null) return BadRequest(ModelState);

				var updateedRestaurant = await _restaurantService.UpdateAnExistingRestaurant(resturantId, restaurantDto);

				return StatusCode(201, updateedRestaurant);
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, $"An error occurred while updating an exisitng Restaurant {nameof(UpdateRestaurant)}.");
				return StatusCode(500, "Internal server error");
			}
		}

	}

}

