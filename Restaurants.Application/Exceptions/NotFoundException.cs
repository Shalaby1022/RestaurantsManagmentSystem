namespace Restaurants.Application.Exceptions
{
	public class NotFoundException : Exception
	{
		public NotFoundException(string resourceType, string resourceIdentifier)
	: base($"{resourceType} with id: {resourceIdentifier} isn't found")
		{
		}

	}
}
