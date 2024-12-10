using FluentValidation;
using Restaurants.Application.Restaurants.DTOS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurants.Application.Restaurants.Validator
{
	public class UpdateRestaurantDtoValidator : AbstractValidator<UpdateRestaurantDto>
	{
		private readonly List<string> validCategories = ["Italian", "Mexican", "Japanese", "Egyptian"];
		public UpdateRestaurantDtoValidator()
		{
			RuleFor(R => R.Name)
			.NotEmpty()
			.WithMessage("Name Is Required")
			.MaximumLength(100)
			.MinimumLength(3);

				RuleFor(R => R.Description)
					.NotEmpty()
					.WithMessage("Description Is Required")
					.Length(3, 500);

				RuleFor(R => R.Category)
					.NotEmpty()
					.WithMessage("Insert Valid Category")

					// Custome Validation For Categories 
					// One way to implement it .Must(validCategories.Contains);
					// Another 
					.Must(categry => validCategories.Contains(categry));

				RuleFor(R => R.ContanctEmail)
					.NotEmpty()
					.EmailAddress()
					.WithMessage("Please Provide Valid Email Address");

				RuleFor(R => R.PhoneNumber)
					.NotEmpty()
					.WithMessage("Please Provide a Valid PhoneNumber");

				RuleFor(R => R.PostalCode)
					.Matches(@"^\d{3}-\d{2}$")
					.WithMessage("Please add a valid postal code matches this XXX-XX");

		}
	}
}
