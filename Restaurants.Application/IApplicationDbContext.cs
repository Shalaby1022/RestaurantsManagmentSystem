using Microsoft.EntityFrameworkCore;
using Restaurants.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurants.Application
{
	public interface IApplicationDbContext
	{
		public DbSet<Restaurant> Restaurants { get; set; }
		public DbSet<Dish> Dishes { get; set; }


		Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken());

	}
}
