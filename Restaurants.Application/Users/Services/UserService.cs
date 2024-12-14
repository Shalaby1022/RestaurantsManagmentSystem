using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Restaurants.Application.Exceptions;
using Restaurants.Application.Users.DTOs;
using Restaurants.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurants.Application.Users.Services
{
	public class UserService : IUserService
	{
		private readonly IUserContext _userContext;
		private readonly IUserStore<ApplicationUser> _userStore;
		private readonly IMapper _mapper;
		private readonly ILogger<UserService> _logger;
		private CancellationToken cancellationToken;

		public UserService(IUserContext userContext ,
							IUserStore<ApplicationUser> userStore ,
							IMapper mapper ,
							ILogger<UserService> logger) 
		{
			_userContext = userContext ?? throw new ArgumentNullException(nameof(userContext));
			_userStore = userStore ?? throw new ArgumentNullException(nameof(userStore));
			_mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
			_logger = logger ?? throw new ArgumentNullException(nameof(logger));
		}
		public async Task<bool> UpdateUserService(UpdateUserDetailsDto userDetails)
		{
			var currentUser = _userContext.GetCurrentUSer();

			if (currentUser == null)
				throw new NotFoundException(nameof(currentUser), "Current user not found");

			var dbUser = await _userStore.FindByIdAsync(currentUser.UserId.ToString(), CancellationToken.None);

			if (dbUser == null)
				throw new NotFoundException(nameof(dbUser), currentUser.UserId.ToString());

			_mapper.Map(userDetails, dbUser);

			var updateResult = await _userStore.UpdateAsync(dbUser, CancellationToken.None);

			if (updateResult.Succeeded)
			{
				_logger.LogInformation($"User {currentUser.UserId} details updated successfully");
				return true;
			}
			else
			{
				_logger.LogWarning($"Failed to update user {currentUser.UserId} details");
				return false;
			}
		}


	}
}
