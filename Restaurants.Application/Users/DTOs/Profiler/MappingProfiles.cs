using AutoMapper;
using Restaurants.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurants.Application.Users.DTOs.Profiler
{
	public class MappingProfiles : Profile
	{
        public MappingProfiles()
        {
            CreateMap<UpdateUserDetailsDto, ApplicationUser>()
                .ForMember(c => c.DateOfBirth, opt => opt.MapFrom(src => src.DateOfBirth))
                .ForMember(C => C.Nationality, opt => opt.MapFrom(src => src.Nationality));

                
        }
    }
}
