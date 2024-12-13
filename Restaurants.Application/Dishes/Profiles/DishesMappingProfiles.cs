using AutoMapper;
using Restaurants.Application.Dishes.Dtos;
using Restaurants.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurants.Application.Dishes.Profiles
{
	public class DishesMappingProfiles : Profile
	{
		public DishesMappingProfiles()
		{
			CreateMap<Dish, GetDishDto>()
			.ReverseMap();

			CreateMap<CreateDishDto , Dish>()
				.ReverseMap();



		}
	}
}
