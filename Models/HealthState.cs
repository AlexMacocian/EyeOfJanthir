using System.Text.Json.Serialization;

namespace EyeOfJanthir.Models;

public record HealthState
{
    [JsonPropertyName("Status")]
    public string Status { get; init; } = ""; // healthy, unhealthy, starting, none
}