using Infrastructure.Common.Extensions;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.SwaggerUI;

namespace WebAPI.Swagger.Configuration
{
    internal class ConfigureSwaggerUIOptions : IConfigureOptions<SwaggerUIOptions>
    {
        private readonly IConfiguration _configuration;

        public ConfigureSwaggerUIOptions(IConfiguration configuration) => _configuration = configuration;

        public void Configure(SwaggerUIOptions options)
        {
            var swaggerSetting = _configuration.GetMyOptions<SwaggerSettings>();

            options.SwaggerEndpoint(
                url: "/swagger/v1/swagger.json",
                name: swaggerSetting.ApiName + "v1");
        }
    }
}
