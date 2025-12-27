using EyeOfJanthir.Models;
using EyeOfJanthir.Services;

namespace EyeOfJanthir.Rules;

public sealed class NotifyEnvironmentChangedUp(
    INotificationService notificationService)
    : IPortainerStateRule
{
    private readonly INotificationService notificationService = notificationService;

    public async Task Execute(PortainerState currentState, PortainerState? previousState, CancellationToken cancellationToken)
    {
        if (previousState is null)
        {
            // Do not notify that an env is up if we have no previous state to compare to.
            return;
        }

        foreach(var endpoint in currentState.Endpoints)
        {
            var previousEndpoint = previousState.Endpoints.FirstOrDefault(e => e.Id == endpoint.Id);
            if (previousEndpoint is null)
            {
                // New endpoint, do not notify.
                continue;
            }

            if (endpoint.Status is PortainerEndpointStatus.Up &&
                previousEndpoint.Status is not PortainerEndpointStatus.Up)
            {
                await this.notificationService.SendNotificationAsync(
                    title: $"Environment {endpoint.Name} is up",
                    message: $"Environment {endpoint.Id} - {endpoint.Name} is up",
                    color: DiscordColor.DiscordGreen,
                    cancellationToken);
            }
        }
    }
}
