using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurants.Application.Parameters.Validator
{
	using FluentValidation;

	public class ResourceParametersValidator : AbstractValidator<ResourceParameters>
	{
		private int[] ValidPageSize = { 5, 10, 15 };

		public ResourceParametersValidator()
		{
			RuleFor(r => r.PageNumber)
				.GreaterThanOrEqualTo(1)
				.WithMessage("Page Number must be greater than or equal to 1.");

			RuleFor(r => r.PageSize)
				.GreaterThanOrEqualTo(1)
				.Must(value => ValidPageSize.Contains(value))
				.WithMessage($"Page Size must be one of the following values: {string.Join(", ", ValidPageSize)}");

			RuleFor(r => r.PageSize)
				.LessThanOrEqualTo(ValidPageSize.Max())
				.WithMessage($"Page Size cannot exceed {ValidPageSize.Max()}.");
		}
	}
}

