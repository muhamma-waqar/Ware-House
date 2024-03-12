using MyWarehouse.Infrastructure;
using MyWarehouse.WebApi.Logging;
using System.Reflection;
var builder = WebApplication.CreateBuilder(args);


builder.Host
    .AddMySerilogLogging()

