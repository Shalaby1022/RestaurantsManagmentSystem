using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurants.Application.Users.DTOs
{
	public class AssignRolesTOUSersDto
	{
		public string UserId { get ; set; }
		public List<string> Roles { get; set; }
	}
}
