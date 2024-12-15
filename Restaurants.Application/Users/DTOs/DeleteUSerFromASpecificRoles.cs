using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurants.Application.Users.DTOs
{
	public class DeleteUSerFromASpecificRoles
	{
		public string Email { get; set; }
		public List<string> Roles { get; set; }
	}
}
