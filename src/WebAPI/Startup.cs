using Application;
using Application.Common.Behaviors;
using Infrastructure;
using WebAPI.API;
using WebAPI.Authentication;
using WebAPI.CORS;
using WebAPI.ErrorHandling;
using WebAPI.Logging;
using WebAPI.Swagger;
using WebAPI.Versioning;

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
            //services.AddMyErrorHandling();
            services.AddSwagger(Configuration);
            //services.AddMyVersioning();
            //services.AddMyCorsConfiguration(Configuration);
            services.AddMyInfrastructureDependencies(Configuration, Environment);
            services.AddMayApplicationDependencies();
        }


        public void Configure(IApplicationBuilder app)
        {
          //  app.UseMyRequestLogging();  // is ki loggingStartup likh ra ta.
            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseMyCorsConfiguration();
            app.UseMySwagger(Configuration);
            app.UseMyInfrastructure(Configuration);
            app.UseMyApi();
        }
    }
}
