using Microsoft.AspNetCore.Mvc.Versioning;
using Swashbuckle.AspNetCore.Swagger;

namespace WebAPI.Versioning
{
    internal static class VersioningStartup
    {
        public static void AddMyVersioning(this IServiceCollection services)
        {
            services.AddApiVersioning(options =>
            {
                options.ApiVersionSelector = new CurrentImplementationApiVersionSelector(options);
            });

            if(services.Any(x => x.ServiceType == typeof(ISwaggerProvider)))
            {
                services.AddVersionedApiExplorer(options =>
                {
                    options.GroupNameFormat = "'v'VVV";
                    options.SubstituteApiVersionInUrl = true;
                    options.AddApiVersionParametersWhenVersionNeutral = true;
                });


            }
        }
    }
}
