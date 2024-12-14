using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Restaurants.Application.Users
{
	internal class UserContext(IHttpContextAccessor httpContextAccessor) : IUserContext
	{
		public CurrentUser GetCurrentUSer()
		{
			var user = httpContextAccessor?.HttpContext?.User;
			if (user == null)
			{
				throw new InvalidOperationException("User isn't Present");
			}
			if (user.Identity == null || !user.Identity.IsAuthenticated)
			{
				return null;
				throw new InvalidOperationException("User isn't Authenticated");
			}

			var userId = user.FindFirst(c => c.Type == ClaimTypes.NameIdentifier)!.Value;
			var email = user.FindFirst(c => c.Type == ClaimTypes.Email)!.Value;
			var roles = user.Claims.Where(c => c.Type == ClaimTypes.Role)!.Select(c => c.Value).ToList();

			return new CurrentUser(userId, email, roles);
		}
	}
}
