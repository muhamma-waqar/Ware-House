using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Common.Extensions
{
    public static class ConfigurationExtensions
    {
        const string ConfigurationErrorMessage = "Options tyoe of '{0}' was requested as required, but the corresponding section was not found in configuration. Make sure one of your configuration sources contains this section.";


        public static T GetMyOptions<T>(this IConfiguration configuration, bool requiredToExistInConfiguration = false) where T : class, new()
        {
            var bound = configuration.GetSection(typeof(T).Name).Get<T>();

            if (bound is null && requiredToExistInConfiguration)
                throw new InvalidOperationException(string.Format(ConfigurationErrorMessage, typeof(T).Name));
            bound ??= new T();
            Validator.ValidateObject(bound, new ValidationContext(bound), validateAllProperties: true);
            return bound;
        }

        public static void RegisterMyOptions<T>(this IServiceCollection services, bool requiredToExistInConfiguration = true) where T : class
        {
            var optionsBuilder = services.AddOptions<T>()
                .BindConfiguration(typeof(T).Name)
                .ValidateDataAnnotations()
                .ValidateOnStart();

            if (requiredToExistInConfiguration)
                optionsBuilder.Validate<IConfiguration>((_, configuration)
                    => configuration.GetSection(typeof(T).Name).Exists(), string.Format(ConfigurationErrorMessage, typeof(T).Name));
            services.AddSingleton(resolver => resolver.GetRequiredService<IOptions<T>>().Value);
        }
    }
}
