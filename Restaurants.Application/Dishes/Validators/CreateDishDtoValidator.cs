using FluentValidation;
using Restaurants.Application.Dishes.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurants.Application.Dishes.Validators
{
	public class CreateDishDtoValidator : AbstractValidator<CreateDishDto>
	{
        public CreateDishDtoValidator()
        {
                RuleFor(n=>n.Name)
                .NotEmpty()
				.WithMessage("Name Is Required")
				.MaximumLength(100)
				.MinimumLength(3);

			RuleFor(n=>n.Description)
				.NotEmpty()
				.WithMessage("Description Is Required")
				.Length(3, 500);

			RuleFor(n => n.Price)
				.NotEmpty()
				.GreaterThanOrEqualTo(0)
				.WithMessage("Price Should be a postitive value and NON-ngeative one");

			RuleFor(n => n.KiloCalories)
				 .NotEmpty()
				 .GreaterThanOrEqualTo(0)
				 .WithMessage("Calories Associated Must Be Greater Than 0");
		}
    }
}
