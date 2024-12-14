using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurants.Application.Users
{
	public record CurrentUser(string UserId , string Email , IEnumerable<string> Roles)
	{
		public bool IsInRoles(string role)
		{
			Roles.Contains(role);
			return true;

		}
    }
}
