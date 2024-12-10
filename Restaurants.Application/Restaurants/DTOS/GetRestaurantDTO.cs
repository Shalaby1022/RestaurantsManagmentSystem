using Restaurants.Application.Dishes.Dtos;
using Restaurants.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurants.Application.Restaurants.DTOS
{
	public class GetRestaurantDTO
	{
		public int Id { get; set; }
		public string Name { get; set; } = string.Empty;
		public string Description { get; set; } = string.Empty;
		public string Category { get; set; } = string.Empty;
		public bool HasDeleivery { get; set; }

		public string Street { get; set; } = string.Empty;
		public string City { get; set; } = string.Empty;
		public string Country { get; set; } = string.Empty;
		public string PostalCode { get; set; } = string.Empty;
		public string PhoneNumber { get; set; } = string.Empty;


		public List<GetDishDto> Dishes { get; set; } = new List<GetDishDto>();
	}
}

