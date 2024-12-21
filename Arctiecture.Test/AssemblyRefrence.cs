using System.Reflection;

namespace Arctiecture.Test
{
	public static class AssemblyReference
	{
		public static Assembly Domain => typeof(Restaurants.Domain.RefrenceForTestPurpose.AssemblyReference).Assembly; 
		public static Assembly Application => typeof(Restaurants.Application.RefrenceForTestPurpose.AssemblyRefrence).Assembly;
		public static Assembly Infrastructure => typeof(Restaurants.Infrastructure.RefrenceForTestPurpose.AssemblyRefrence).Assembly;
		public static Assembly API => typeof(Restaurants.API.RefrenceForTestPurpose.AssemblyReference).Assembly;
	}
}
