using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Restaurants.Application.Users.DTOs;
using Restaurants.Application.Users.Services;

namespace Restaurants.API.Controllers
{
	[ApiController]
	[Route("identity")]
	
	public class IdentityController : ControllerBase
	{
		private readonly ILogger<IdentityController> _logger;
		private readonly IUserService _userService;

		public IdentityController(ILogger<IdentityController> logger ,
									IUserService userService)
        {
			_logger = logger ??throw new ArgumentNullException(nameof(logger));	
			_userService = userService ?? throw new ArgumentNullException(nameof(userService));
		}


		[Authorize]
		[HttpPatch("user")]
		[ProducesResponseType(StatusCodes.Status204NoContent)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status406NotAcceptable)]
		[ProducesResponseType(StatusCodes.Status415UnsupportedMediaType)]
		[ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]
		
		public async Task<IActionResult> UpdateUserDetails(UpdateUserDetailsDto updateUserDetailsDto)
		{
			if (!ModelState.IsValid)
			{
				_logger.LogError($"Invalid Update attempt in {nameof(UpdateUserDetails)}");
				return BadRequest(ModelState);
			}

			var userFound = await _userService.UpdateUserService(updateUserDetailsDto);

			return NoContent();
		}

	}
}
												