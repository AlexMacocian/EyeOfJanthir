namespace EyeOfJanthir.Models;

public record ContainerHealthSummary
{
    public required string ContainerId { get; init; }
    public required string Name { get; init; }
    public required string State { get; init; }
    public required bool IsRunning { get; init; }
    public required string HealthStatus { get; init; }
    public string? StartedAt { get; init; }
    public int RestartCount { get; init; }
}