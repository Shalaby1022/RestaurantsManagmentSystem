using NetArchTest.Rules;


namespace Arctiecture.Test
{
	public class ArchitectureTests
	{
		private const string DomainNamespace = "Restaurants.Domain.RefrenceForTestPurpose";
		private const string InfrastructureNamespace = "Restaurants.Infrastructure.RefrenceForTestPurpose";
		private const string ApplicationNamespace = "Restaurants.Application.RefrenceForTestPurpose";
		private const string APINamespace = "Restaurants.API.RefrenceForTestPurpose";

		[Fact]
		public void Domain_Should_Not_HaveDependencyOnOtherProject()
		{
			// Arrange 
			var domainAssembly = typeof(Restaurants.Domain.RefrenceForTestPurpose.AssemblyReference).Assembly;

			var otherProjects = new[]
			{
				InfrastructureNamespace,
				ApplicationNamespace,
				APINamespace
			};

			// Act 
			var testResult = Types
				.InAssembly(domainAssembly)
				.ShouldNot()
				.HaveDependencyOnAny(otherProjects) 
				.GetResult();

			// Assert 
			Assert.True(testResult.IsSuccessful);

		}

		[Fact]
		public void Application_Should_Not_HaveDependencyOnOtherProject()
		{
			// Arrange 
			var applicationAssembly = typeof(Restaurants.Application.RefrenceForTestPurpose.AssemblyRefrence).Assembly;

			var otherProjects = new[]
			{
				InfrastructureNamespace,
				APINamespace
			};

			// Act 
			var testResult = Types
				.InAssembly(applicationAssembly)
				.ShouldNot()
				.HaveDependencyOnAny(otherProjects)
				.GetResult();

			// Assert 
			Assert.True(testResult.IsSuccessful);

		}

		[Fact]
		public void Infrastructure_Should_Not_HaveDependencyOnOtherProject()
		{
			// Arrange 
			var infrastructureAssembly = typeof(Restaurants.Infrastructure.RefrenceForTestPurpose.AssemblyRefrence).Assembly;

			var otherProjects = new[]
			{
				APINamespace
			};

			// Act 
			var testResult = Types
				.InAssembly(infrastructureAssembly)
				.ShouldNot()
				.HaveDependencyOnAny(otherProjects)
				.GetResult();

			// Assert 
			Assert.True(testResult.IsSuccessful);
		}
	}
}
