using EyeOfJanthir.Extensions;
using Microsoft.Extensions.Hosting;

Host.CreateApplicationBuilder(args)
    .WithEnvironmentVariables()
    .WithPortainerServices()
    .WithPortainerStateRules()
    .WithConsoleLogging()
    .WithNotifications()
    .Build()
    .Run();