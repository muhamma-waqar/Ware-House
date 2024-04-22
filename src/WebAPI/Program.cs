using Infrastructure;
using Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using WebAPI;
using WebAPI.Logging;
var builder = WebApplication.CreateBuilder(args);


builder.Host
    .ConfigureAppConfiguration((context, config) =>
    {
        config.AddUserSecrets(Assembly.GetEntryAssembly(), optional: true);
        config.AdMyInfrastructureConfiguration(context);
    });
builder.Services.AddDbContext<ApplicationDbContext>(option =>
option.UseSqlServer(builder.Configuration["ConnectionStrings:DefaultConnection"]));
var startup = new Startup(builder.Configuration, builder.Environment);
startup.ConfigureService(builder.Services);
var app = builder.Build();

startup.Configure(app);
app.Run();
