using Microsoft.AspNetCore.Mvc;
using Restaurants.Application.Restaurants;

namespace Restaurants.API.Controllers
{
	[ApiController]
	[Route("restaurant")]
	[Produces("application/json", "application/xml")]
	[Consumes("application/json", "application/xml")]

	public class RestaurantController : ControllerBase
	{
		private readonly IRestaurantService _restaurantService;
		private readonly ILogger<RestaurantController> _logger;

		public RestaurantController(IRestaurantService restaurantService , 
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

			if(restaurant == null)
			{
				return NotFound();
			}

			return Ok(restaurant);
		}


	}
}

