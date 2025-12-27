using EyeOfJanthir.Models;
using EyeOfJanthir.Options;
using EyeOfJanthir.Services;
using Microsoft.Extensions.Options;

namespace EyeOfJanthir.Rules;

public sealed class NotifyEyeOfJanthirUp(
    INotificationService notificationService,
    IOptions<PortainerOptions> options)
    : IPortainerStateRule
{
    private readonly INotificationService notificationService = notificationService;
    private readonly PortainerOptions discordOptions = options.Value;
    private bool executed = false;

    public async Task Execute(PortainerState currentState, PortainerState? previousState, CancellationToken cancellationToken)
    {
        if (this.executed)
        {
            return;
        }

        await this.notificationService.SendNotificationAsync(
            title: "Eye of Janthir is up",
            message: $"Eye of Janthir is up and monitoring {options.Value.Url} every {options.Value.Frequency}",
            color: DiscordColor.DiscordGreen,
            cancellationToken);
        this.executed = true;
    }
}
