namespace EyeOfJanthir.Services;

public interface INotificationService
{
    Task SendNotificationAsync(string title, string message, CancellationToken cancellationToken);
}
