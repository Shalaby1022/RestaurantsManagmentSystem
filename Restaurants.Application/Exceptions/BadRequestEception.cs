using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurants.Application.Exceptions
{
	public class BadRequestEception : Exception
	{
		public BadRequestEception(string resourceType, string resourceIdentifier)
			: base($"Invalid request: The provided {resourceType} identifier '{resourceIdentifier}' is invalid or malformed.") { }
	}
}
