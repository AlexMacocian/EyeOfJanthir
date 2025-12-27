using EyeOfJanthir.Models;
using EyeOfJanthir.Services;

namespace EyeOfJanthir.Rules;

public sealed class NotifyEnvironmentIsDown(
    INotificationService notificationService)
    : IPortainerStateRule
{
    private readonly INotificationService notificationService = notificationService;

    public async Task Execute(PortainerState currentState, PortainerState? previousState, CancellationToken cancellationToken)
    {
        foreach(var endpoint in currentState.Endpoints)
        {
            var previousStateEndpoint = previousState?.Endpoints.FirstOrDefault(e => e.Id == endpoint.Id);
            if (endpoint.Status is not PortainerEndpointStatus.Up &&
                (previousStateEndpoint is null || previousStateEndpoint.Status is PortainerEndpointStatus.Up))
            {
                await this.notificationService.SendNotificationAsync(
                    title: $"Environment {endpoint.Name} is down",
                    message: $"Environment {endpoint.Id} - {endpoint.Name} is down",
                    color: DiscordColor.Red,
                    cancellationToken);
            }
        }
    }
}
