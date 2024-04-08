using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace WebAPI.Versioning.SwaggerConfiguration
{
    public class ConfigureSwaggerGenOptions : IPostConfigureOptions<SwaggerGenOptions>
    {
        readonly IApiVersionDescriptionProvider _versionProvider;

        public ConfigureSwaggerGenOptions(IApiVersionDescriptionProvider versionProvider) => _versionProvider = versionProvider;

        public void PostConfigure(string _, SwaggerGenOptions options)
        {
            options.SwaggerGeneratorOptions.SwaggerDocs.Clear();

            foreach(var description in _versionProvider.ApiVersionDescriptions)
            {
                options.SwaggerDoc(
                    description.GroupName,
                    new Microsoft.OpenApi.Models.OpenApiInfo()
                    {
                        Title = $"{nameof(MyWarehouse)} {description.ApiVersion}",
                        Version = description.ApiVersion.ToString(),
                    });
            }
        }
    }
}
