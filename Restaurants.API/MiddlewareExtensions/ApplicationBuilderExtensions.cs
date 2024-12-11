using Restaurants.API.Middlewares;

namespace Restaurants.API.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder AddGLobalErrorHandler(this IApplicationBuilder app) => 
            app.UseMiddleware<GlobalErrorHandlingMiddleware>();
    }
}
