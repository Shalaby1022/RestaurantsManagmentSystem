﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurants.Domain.Entities
{
	public class Dish
	{
		public int Id { get; set; }
		public string Name { get; set; } = string.Empty;
		public string Description { get; set; } = string.Empty;
		public int? KiloCalories { get; set; }
		public decimal Price { get; set; }



		public Restaurant? Restaurant { get; set; }
		public int RestaurantId { get; set; }
	}
}
