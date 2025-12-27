using System.Text.Json.Serialization;

namespace EyeOfJanthir.Models;

public record ContainerInspect
{
    [JsonPropertyName("Id")]
    public string Id { get; init; } = "";

    [JsonPropertyName("Name")]
    public string Name { get; init; } = "";

    [JsonPropertyName("State")]
    public ContainerState? State { get; init; }

    [JsonPropertyName("RestartCount")]
    public int RestartCount { get; init; }
}