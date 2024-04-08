using Infrastructure.Common.Extensions;
using WebAPI.CORS.Settings;

namespace WebAPI.CORS
{
    internal static class CorsStartup
    {
        public static void AddMyCorsConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            var corsSetting = configuration.GetMyOptions<CorsSettings>();

            if(corsSetting == null) 
            {
                return;
            }

            services.AddCors(option =>
            {
                option.AddDefaultPolicy(builder =>
                {
                    builder.AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials()
                    .SetIsOriginAllowedToAllowWildcardSubdomains()
                    .WithOrigins(corsSetting.AllowedOrigins)
                    .Build();
                });
            });
        }
        public static void UseMyCorsConfiguration(this IApplicationBuilder app)
        {
            app.UseCors();
        }
    }
}
