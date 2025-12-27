using System.Text.Json.Serialization;

namespace EyeOfJanthir.Models;

public record ContainerInfo
{
    [JsonPropertyName("Id")]
    public string Id { get; init; } = "";

    [JsonPropertyName("Names")]
    public List<string> Names { get; init; } = [];

    [JsonPropertyName("Image")]
    public string Image { get; init; } = "";

    [JsonPropertyName("State")]
    public string State { get; init; } = "";

    [JsonPropertyName("Status")]
    public string Status { get; init; } = "";
}
