using System.Text.Json.Serialization;

namespace EyeOfJanthir.Models;

public record ContainerState
{
    [JsonPropertyName("Status")]
    public string Status { get; init; } = "";

    [JsonPropertyName("Running")]
    public bool Running { get; init; }

    [JsonPropertyName("Paused")]
    public bool Paused { get; init; }

    [JsonPropertyName("Restarting")]
    public bool Restarting { get; init; }

    [JsonPropertyName("Dead")]
    public bool Dead { get; init; }

    [JsonPropertyName("StartedAt")]
    public string? StartedAt { get; init; }

    [JsonPropertyName("Health")]
    public HealthState? Health { get; init; }
}