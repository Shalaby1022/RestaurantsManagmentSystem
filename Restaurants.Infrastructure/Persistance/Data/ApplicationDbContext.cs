using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Restaurants.Application;
using Restaurants.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurants.Infrastructure.Persistance.Data
{
	public sealed class ApplicationDbContext : IdentityDbContext<ApplicationUser>, IApplicationDbContext
	{
		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
		{
		}
		public DbSet<Restaurant> Restaurants { get; set; } 
		public DbSet<Dish> Dishes { get; set; } 

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			if (modelBuilder is null)
				throw new ArgumentNullException(nameof(modelBuilder));

			modelBuilder.Entity<Restaurant>()
				.OwnsOne(A => A.Address);

			modelBuilder.Entity<Restaurant>()
				.HasMany(r => r.Dishes)
				.WithOne(d => d.Restaurant)
				.HasForeignKey(d => d.RestaurantId);

			base.OnModelCreating(modelBuilder);
			modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);

		}

	}
}
