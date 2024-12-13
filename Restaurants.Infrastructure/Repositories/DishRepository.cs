using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Restaurants.Application;
using Restaurants.Application.Exceptions;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurants.Infrastructure.Repositories
{
	public class DishRepository : IDishRepository
	{
		private readonly IApplicationDbContext _dbContext;
		private readonly ILogger<DishRepository> _logger;

		public DishRepository(IApplicationDbContext dbContext,
									ILogger<DishRepository> logger)
		{
			_dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
			_logger = logger ?? throw new ArgumentNullException(nameof(logger));
		}
		public async Task<Dish> CreateNewDishAsync(Dish dish)
		{
			if (dish == null) throw new NotFoundException(nameof(dish) , dish.Id.ToString());

			await _dbContext.Dishes.AddAsync(dish);
			await _dbContext.SaveChangesAsync();
			return dish;
		}

		public async Task<bool> DeleteRestaurantAsync(int Id)
		{
			var dishDeleted = await  _dbContext.Dishes.FirstOrDefaultAsync(x => x.Id == Id);

			if (dishDeleted == null)
				throw new NotFoundException(nameof(Dish), Id.ToString());

			 _dbContext.Dishes.Remove(dishDeleted);
			await _dbContext.SaveChangesAsync();
			return true;
		}

		public async Task<IEnumerable<Dish>> GetAllDishesAsync()
		{
			var dishesFromDb = await _dbContext.Dishes
										  .ToListAsync();
			return dishesFromDb;
		}

		public async Task<Dish> GetDishByIdAsync(int id)
		{
			var dish = await _dbContext.Dishes.FirstOrDefaultAsync(x => x.Id == id);
			return dish == null ? null : dish;
		}
	}
}
