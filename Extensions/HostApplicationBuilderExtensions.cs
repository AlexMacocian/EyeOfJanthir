using DotNetEnv.Configuration;
using EyeOfJanthir.Options;
using EyeOfJanthir.Rules;
using EyeOfJanthir.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace EyeOfJanthir.Extensions;

public static class HostApplicationBuilderExtensions
{
    public static HostApplicationBuilder WithEnvironmentVariables(this HostApplicationBuilder hostApplicationBuilder)
    {
        hostApplicationBuilder.Configuration.AddDotNetEnv(".env");
        return hostApplicationBuilder;
    }

    public static HostApplicationBuilder WithConsoleLogging(this HostApplicationBuilder hostApplicationBuilder)
    {
        hostApplicationBuilder.Logging.AddConsole();
        return hostApplicationBuilder;
    }

    public static HostApplicationBuilder WithPortainerServices(this HostApplicationBuilder hostApplicationBuilder)
    {
        hostApplicationBuilder.Services.AddOptions<PortainerOptions>()
            .Bind(hostApplicationBuilder.Configuration.GetSection("Portainer"))
            .Validate(opts => !string.IsNullOrWhiteSpace(opts.ApiKey) && !string.IsNullOrWhiteSpace(opts.Url))
            .ValidateOnStart();

        hostApplicationBuilder.Services.AddSingleton<PortainerService>();
        hostApplicationBuilder.Services.AddHostedService<PortainerHostedService>();

        hostApplicationBuilder.Services.AddHttpClient(nameof(PortainerService), (sp, client) =>
        {
            var options = sp.GetRequiredService<IOptions<PortainerOptions>>();
            if (!Uri.TryCreate(options.Value.Url, UriKind.Absolute, out var portainerUrl))
            {
                throw new InvalidOperationException($"Failed to load portainer url {options.Value.Url}");
            }

            client.BaseAddress = portainerUrl;
            client.Timeout = options.Value.Frequency;
            client.DefaultRequestHeaders.TryAddWithoutValidation("X-API-Key", options.Value.ApiKey);
        }).AddDefaultLogger();
        return hostApplicationBuilder;
    }

    public static HostApplicationBuilder WithPortainerStateRules(this HostApplicationBuilder hostApplicationBuilder)
    {
        hostApplicationBuilder.Services.AddSingleton<IPortainerStateRule, NotifyEnvironmentIsDown>();
        hostApplicationBuilder.Services.AddSingleton<IPortainerStateRule, NotifyEnvironmentChangedUp>();
        hostApplicationBuilder.Services.AddSingleton<IPortainerStateRule, NotifyContainerIsDown>();
        hostApplicationBuilder.Services.AddSingleton<IPortainerStateRule, NotifyContainerChangedUp>();

        return hostApplicationBuilder;
    }

    public static HostApplicationBuilder WithNotifications(this HostApplicationBuilder hostApplicationBuilder)
    {
        // TODO: Change to Discord notification service once implemented
        hostApplicationBuilder.Services.AddSingleton<INotificationService, LoggingNotificationService>();

        return hostApplicationBuilder;
    }
}
