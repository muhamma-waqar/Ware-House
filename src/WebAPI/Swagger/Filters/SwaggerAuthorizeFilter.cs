using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using WebAPI.Swagger.Configuration;

namespace WebAPI.Swagger.Filters
{
    [ExcludeFromCodeCoverage]
    public class SwaggerAuthorizeFilter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            var filterDescripter = context.ApiDescription.ActionDescriptor.FilterDescriptors;

            var hasAuthorizeFilter = filterDescripter.Select(filterInfo => filterInfo.Filter).Any(filter => filter is AuthorizeFilter);
            var allowAnonymous = filterDescripter.Select(filterInfo=> filterInfo.Filter).Any(filter => filter is IAllowAnonymousFilter);
            var hasAuthorizeAttribute = context.MethodInfo.DeclaringType!.GetCustomAttributes(true).Any(attr => attr is AuthorizeAttribute)
                || context.MethodInfo.GetCustomAttributes(true).Any(attr => attr is AuthorizeAttribute);

            if((hasAuthorizeAttribute || hasAuthorizeAttribute) && !allowAnonymous) 
            {
                operation.Responses.Add("401", new OpenApiResponse { Description = "Unauthorized" });
                operation.Responses.Add("403", new OpenApiResponse { Description = "Forbidden" });

                if(operation.Security == null)
                {
                    operation.Security = new List<OpenApiSecurityRequirement>();
                }

                operation.Security.Add(new OpenApiSecurityRequirement(){
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = SecuritySchemeNames.ApiLogin
                            },
                            Scheme = "oauth2",
                            Name = "Bearer",
                            In = ParameterLocation.Header,
                        },
                        new List<string>()
                    }
                }
                );
            }
        }
    }
}
