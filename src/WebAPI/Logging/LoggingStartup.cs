using Infrastructure.Common.Extensions;
using Serilog;
using Serilog.Events;
using WebAPI.Logging.Helper;
using WebAPI.Logging.Settings;

namespace WebAPI.Logging
{
    internal static class LoggingStartup
    {
        public static IHostBuilder AddMySerilogLogging(this IHostBuilder webBuilder)
        {
            return webBuilder.UseSerilog((context, loggerCfg) =>
            {
                loggerCfg
                .MinimumLevel.Information()
                .Enrich.FromLogContext()
                .Enrich.WithProperty("EnvironmentName", context.HostingEnvironment.EnvironmentName)
                .Enrich.WithMachineName();

                if (context.HostingEnvironment.IsDevelopment())
                {
                    loggerCfg
                    .WriteTo.Console()
                    .WriteTo.Debug();
                }
                else
                {
                    loggerCfg
                    .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
                    .WriteTo.Console(restrictedToMinimumLevel: LogEventLevel.Error);
                }

                var loggingSettings = context.Configuration.GetMyOptions<LogglySettings>();
                if (loggingSettings.WriteToLoggly.GetValueOrDefault() == true)
                {
                    loggerCfg.WriteTo.Loggly(
                        customerToken: loggingSettings.CustomerToken);
                }
            });
        }

        public static IApplicationBuilder UseMyRequestLogging(this IApplicationBuilder appBuilder) 
        {
            return appBuilder
                .UseSerilogRequestLogging(opts => opts.GetLevel = LogHelper.ExcludeHealthChecks);
        }
    }
}
