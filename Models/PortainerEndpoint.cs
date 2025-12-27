using System.Text.Json.Serialization;

namespace EyeOfJanthir.Models;

public record PortainerEndpoint
{
    [JsonPropertyName("Id")]
    public int Id { get; init; }

    [JsonPropertyName("Name")]
    public string Name { get; init; } = "";

    [JsonPropertyName("URL")]
    public string Url { get; init; } = "";

    [JsonPropertyName("Status")]
    public int Status { get; init; }
}