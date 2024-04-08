using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;
using Swashbuckle.AspNetCore.SwaggerUI;
using WebAPI.Versioning.SwaggerConfiguration;

namespace WebAPI.Versioning
{
    internal static class VersioningStartup
    {
        public static void AddMyVersioning(this IServiceCollection services)
        {
            services.AddApiVersioning(option =>
            {
                option.ApiVersionSelector = new CurrentImplementationApiVersionSelector(option);
            });

            // If swagger is enabled add versioning support to it.

            if(services.Any(x => x.ServiceType == typeof(ISwaggerProvider)))
            {
                services.AddVersionedApiExplorer(option =>
                {
                    option.GroupNameFormat = "'v'VVV";
                    option.SubstituteApiVersionInUrl = true;
                    option.AddApiVersionParametersWhenVersionNeutral = true;
                });

                // Override swagger configuration with versioned ones.

                services.AddTransient<IPostConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerGenOptions>();
                services.AddTransient<IPostConfigureOptions<SwaggerUIOptions>, ConfigureSwaggerUIOptions>();
            }
        }
    }
}
