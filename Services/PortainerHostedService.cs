using EyeOfJanthir.Models;
using EyeOfJanthir.Options;
using EyeOfJanthir.Rules;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Collections.Immutable;

namespace EyeOfJanthir.Services;

public sealed class PortainerHostedService(
    IEnumerable<IPortainerStateRule> portainerStateRules,
    PortainerService portainerService,
    IOptions<PortainerOptions> portainerOptions,
    ILogger<PortainerHostedService> logger)
    : IHostedService
{
    private readonly IEnumerable<IPortainerStateRule> rules = portainerStateRules;
    private readonly PortainerService portainerService = portainerService;
    private readonly PortainerOptions portainerOptions = portainerOptions.Value;
    private readonly ILogger<PortainerHostedService> logger = logger;

    private PortainerState? previousState;

    public Task StartAsync(CancellationToken cancellationToken)
    {
        return this.PeriodicallyCheckPortainer(cancellationToken);
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }

    private async Task PeriodicallyCheckPortainer(CancellationToken cancellationToken)
    {
        this.logger.LogInformation("Starting periodic Portainer checks...");
        while (!cancellationToken.IsCancellationRequested)
        {
            try
            {
                var endpoints = await this.portainerService.GetEndpointsAsync(cancellationToken);
                var tasks = (endpoints ?? [])
                    .Select(async endpoint => (
                        Endpoint: endpoint,
                        Containers: await this.portainerService.GetContainersAsync(endpoint, cancellationToken)
                    ));

                var mapping = await Task.WhenAll(tasks);
                var currentState = new PortainerState
                {
                    Endpoints = endpoints?.ToImmutableList() ?? [],
                    Environments = [.. mapping.Select(m => (m.Endpoint, m.Containers?.ToImmutableList()))]
                };

                foreach(var rule in this.rules)
                {
                    await rule.Execute(currentState, this.previousState, cancellationToken);
                }

                this.previousState = currentState;
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, "Encountered exception while checking portainer");
            }
            finally
            {
                await Task.Delay(this.portainerOptions.Frequency, cancellationToken);
            }
        }
    }
}
