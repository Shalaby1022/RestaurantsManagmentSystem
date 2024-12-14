using FluentValidation;
using Restaurants.Application.Restaurants.DTOS;
using Restaurants.Application.Users.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurants.Application.Users.Validator
{
	public class UpdateUserDetailsDtoValidator : AbstractValidator<UpdateUserDetailsDto>
	{
		public UpdateUserDetailsDtoValidator()
		{
			RuleFor(c => c.DateOfBirth)
			.NotEmpty().WithMessage("DateTime is required.");


			RuleFor(c => c.Nationality)
			.NotEmpty().WithMessage("Nationality is required")
			.MinimumLength(2).WithMessage("Nationality must be at least 2 characters")
			.MaximumLength(50).WithMessage("Nationality cannot exceed 50 characters");


		}


	}
}
