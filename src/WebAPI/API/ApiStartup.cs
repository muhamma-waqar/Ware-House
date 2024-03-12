using System.Text.Json;

namespace WebAPI.API
{
    internal static class ApiStartup
    {
        public static void AddMyApi(this IServiceCollection services)
        {
            services.AddHealthChecks();
            services.AddControllers()
                .AddControllersAsServices()
                .AddJsonOptions(c => c.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase);
        }

        public static void UseMayApi(this IApplicationBuilder app)
        {
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHealthChecks("/health");
            });
        }
    }
}
