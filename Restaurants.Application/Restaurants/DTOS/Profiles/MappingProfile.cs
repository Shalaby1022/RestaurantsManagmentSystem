using AutoMapper;
using Restaurants.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurants.Application.Restaurants.DTOS.Profiles
{
	public class MappingProfile : Profile
	{
        public MappingProfile()
        {
            CreateMap<Restaurant, GetRestaurantDTO>()
                    .ForMember(c => c.City, opt => opt.MapFrom(src => src.Address == null ? null : src.Address.City))
                    .ForMember(s => s.Street, opt => opt.MapFrom(src => src.Address == null ? null : src.Address.Street))
                    .ForMember(p => p.PostalCode, opt => opt.MapFrom(src => src.Address == null ? null : src.Address.PostalCode))
                    .ForMember(c => c.Country, opt => opt.MapFrom(src => src.Address == null ? null : src.Address.Country))
                    .ForMember(p => p.PhoneNumber, opt => opt.MapFrom(src => src.Address == null ? null : src.Address.PhoneNumber))
                    .ForMember(D => D.Dishes, opt => opt.MapFrom(src => src.Dishes));


			CreateMap<CreateRestaurantDto, Restaurant>()
				.ForMember(a => a.Address, opt => opt.MapFrom(
					src => new Address
					{
						Street = src.Street,
						City = src.City,
						PostalCode = src.PostalCode,
						Country = src.Country,
						PhoneNumber = src.PhoneNumber
					}))
				.ReverseMap()
				.ForPath(dest => dest.Street, opt => opt.MapFrom(src => src.Address.Street))
				.ForPath(dest => dest.City, opt => opt.MapFrom(src => src.Address.City))
				.ForPath(dest => dest.PostalCode, opt => opt.MapFrom(src => src.Address.PostalCode))
				.ForPath(dest => dest.Country, opt => opt.MapFrom(src => src.Address.Country))
				.ForPath(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.Address.PhoneNumber));



		}
	}
}
