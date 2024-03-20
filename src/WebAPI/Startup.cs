using WebAPI.API;
using WebAPI.Authentication;
using WebAPI.ErrorHandling;
using WebAPI.Swagger;

namespace WebAPI
{
    public class Startup
    {
        protected IConfiguration Configuration { get; set; }
        protected IWebHostEnvironment Environment { get; set; }

        public Startup(IConfiguration configuration, IWebHostEnvironment environment)
            => (Configuration, Environment) = (configuration, environment);

        public void ConfigureService(IServiceCollection services)
        {
            services.AddMyApi();
            services.AddMyApiAuthDeps();
            services.AddMyErrorHandling();
            services.AddMySwagger(Configuration);

        }
    }
}
