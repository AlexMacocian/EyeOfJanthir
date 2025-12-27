using EyeOfJanthir.Models;
using EyeOfJanthir.Services;

namespace EyeOfJanthir.Rules;

public sealed class NotifyContainerChangedUp(INotificationService notificationService)
    : IPortainerStateRule
{
    private readonly INotificationService notificationService = notificationService;

    public async Task Execute(PortainerState currentState, PortainerState? previousState, CancellationToken cancellationToken)
    {
        foreach ((var endpoint, var containers) in currentState.Environments)
        {
            foreach (var container in containers ?? [])
            {
                var previousContainer = previousState?.Environments
                    .FirstOrDefault(e => e.Endpoint.Id == endpoint.Id)
                    .Containers?
                    .FirstOrDefault(c => c.Id == container.Id);
                if (container.State is "running" &&
                    previousContainer is not null &&
                    previousContainer.State is not "running")
                {
                    await this.notificationService.SendNotificationAsync(
                        title: $"Container {string.Join(", ", container.Names)} is up",
                        message: $"Container {container.Id} - {string.Join(", ", container.Names)} is up. State {container.State}. Status {container.Status}. Image {container.Image}" +
                        $"Environment {endpoint.Name}. Id {endpoint.Id}. Url {endpoint.Url}. Status {endpoint.Status}",
                        color: DiscordColor.DiscordGreen,
                        cancellationToken);
                }
            }
        }
    }
}
