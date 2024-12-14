using Restaurants.Application.Users.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurants.Application.Users.Services
{
	public interface IUserService
	{
		Task<bool> UpdateUserService(UpdateUserDetailsDto userDetails); 
	}
}
