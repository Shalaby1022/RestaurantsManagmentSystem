using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurants.Application.Restaurants.DTOS
{
	public class UpdateRestaurantDto
	{
		public string Name { get; set; } = string.Empty;
		public string Description { get; set; } = string.Empty;
		public string Category { get; set; } = string.Empty;
		public bool HasDeleivery { get; set; }

		public string? ContanctEmail { get; set; }
		public string? ContactNumber { get; set; }

		public string Street { get; set; } = string.Empty;
		public string City { get; set; } = string.Empty;
		public string Country { get; set; } = string.Empty;
		public string PostalCode { get; set; } = string.Empty;
		public string PhoneNumber { get; set; } = string.Empty;
	}
}
