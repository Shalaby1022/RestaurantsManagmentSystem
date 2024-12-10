using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Restaurants.Domain.Entities
{
	public class Restaurant
	{
		public int Id { get; set; }
		public string Name { get; set; } = string.Empty;
		public string Description { get; set; } = string.Empty;
		public string Category { get; set; } = string.Empty;
		public bool HasDeleivery { get; set; }
		public string? ContanctEmail { get; set; }
		public string? ContactNumber { get; set; }

		
		public Address Address { get; set; }

		public List<Dish> Dishes { get; set; } = new List<Dish>();
	}
}
