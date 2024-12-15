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
		private readonly UserManager<ApplicationUser> _userManager;
		private readonly RoleManager<IdentityRole> _roleManager;
		private readonly IMapper _mapper;
		private readonly ILogger<UserService> _logger;
		private CancellationToken cancellationToken;

		public UserService(IUserContext userContext,
				   IUserStore<ApplicationUser> userStore,
				   UserManager<ApplicationUser> userManager,
				   RoleManager<IdentityRole> roleManager,
				   IMapper mapper,
				   ILogger<UserService> logger)
		{
			_userContext = userContext ?? throw new ArgumentNullException(nameof(userContext));
			_userStore = userStore ?? throw new ArgumentNullException(nameof(userStore));
			_userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
			_roleManager = roleManager ?? throw new ArgumentNullException(nameof(roleManager));
			_mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
			_logger = logger ?? throw new ArgumentNullException(nameof(logger));
		}

		public async Task AssignRolesToUsersAsync(AssignRolesTOUSersDto assignRolesToUsersDto)
		{
			if (assignRolesToUsersDto == null)
				throw new NotFoundException(nameof(assignRolesToUsersDto) , assignRolesToUsersDto.UserId);

			if (string.IsNullOrEmpty(assignRolesToUsersDto.UserId))
				throw new ArgumentException("UserId must not be null or empty.", nameof(assignRolesToUsersDto.UserId));

			if (assignRolesToUsersDto.Roles == null || !assignRolesToUsersDto.Roles.Any())
				throw new ArgumentException("Roles must not be null or empty.", nameof(assignRolesToUsersDto.Roles));

			var user = await _userStore.FindByIdAsync(assignRolesToUsersDto.UserId, cancellationToken);

			if (user == null)
			{
				_logger.LogWarning($"User with ID {assignRolesToUsersDto.UserId} not found.");
				throw new NotFoundException(nameof(ApplicationUser), assignRolesToUsersDto.UserId);
			}

			foreach (var role in assignRolesToUsersDto.Roles)
			{
				// Check if the role exists
				var roleExists = await _roleManager.RoleExistsAsync(role);
				if (!roleExists)
				{
					_logger.LogWarning($"Role {role} does not exist.");
					throw new NotFoundException(nameof(IdentityRole), role);
				}

				// Check if the user is already in the role
				var isAlreadyInRole = await _userManager.IsInRoleAsync(user, role);
				if (isAlreadyInRole)
				{
					_logger.LogInformation($"User {user.Id} is already in role {role}.");
					continue; 
				}

				// Assign the role to the user
				var result = await _userManager.AddToRoleAsync(user, role);
				if (!result.Succeeded)
				{
					var errorDetails = string.Join(", ", result.Errors.Select(e => e.Description));
					_logger.LogError($"Failed to assign role {role} to user {user.Id}: {errorDetails}");
					throw new ApplicationException($"Failed to assign role {role} to user {user.Id}: {errorDetails}");
				}
				_logger.LogInformation($"Role {role} assigned to user {user.Id} successfully.");
			}
		}


		public async Task DeleteRolesFromAUserAsync(DeleteUSerFromASpecificRoles RolesToUsersDto)
		{
			if (RolesToUsersDto == null)
				throw new NotFoundException(nameof(RolesToUsersDto), RolesToUsersDto.Email);

			if (string.IsNullOrEmpty(RolesToUsersDto.Email))
				throw new ArgumentException("UserId must not be null or empty.", nameof(RolesToUsersDto.Email));

			if (RolesToUsersDto.Roles == null || !RolesToUsersDto.Roles.Any())
				throw new ArgumentException("Roles must not be null or empty.", nameof(RolesToUsersDto.Roles));

			var user = await _userManager.FindByEmailAsync(RolesToUsersDto.Email);

			if (user == null)
			{
				_logger.LogWarning($"User with ID {RolesToUsersDto.Email} not found.");
				throw new NotFoundException(nameof(ApplicationUser), RolesToUsersDto.Email);
			}

			foreach (var role in RolesToUsersDto.Roles)
			{
				// Check if the role exists
				var roleExists = await _roleManager.RoleExistsAsync(role);
				if (!roleExists)
				{
					_logger.LogWarning($"Role {role} does not exist.");
					throw new NotFoundException(nameof(IdentityRole), role);
				}

				// Check if the user is already in the role
				var isAlreadyInRole = await _userManager.IsInRoleAsync(user, role);
				if (isAlreadyInRole)
				{
					_logger.LogInformation($"User {user.Id} is already in role {role}.");
					continue;
				}

				var result = await _userManager.RemoveFromRoleAsync(user, role);
				if (!result.Succeeded)
				{
					var errors = string.Join(", ", result.Errors.Select(e => e.Description));
					_logger.LogError($"Failed to remove user {user.Id} from role {role}. Errors: {errors}");
					throw new Exception($"Failed to remove user from role {role}. Errors: {errors}");
				}

				_logger.LogInformation($"Successfully removed user {user.Id} from role {role}.");
		}
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
