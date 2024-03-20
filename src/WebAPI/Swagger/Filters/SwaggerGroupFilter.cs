using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace WebAPI.Swagger.Filters
{
    public class SwaggerGroupFilter : IOperationFilter
    {
        void IOperationFilter.Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            var customGroupAttribute = context.MethodInfo.DeclaringType!.GetCustomAttributes(true)
                .OfType<SwaggerGroupAttribute>()?.FirstOrDefault();
            if(customGroupAttribute != null && !string.IsNullOrWhiteSpace(customGroupAttribute.GroupName)) 
            {
                operation.Tags = new List<OpenApiTag> { new OpenApiTag { Name = customGroupAttribute.GroupName } };
            }

        }
    }
}
