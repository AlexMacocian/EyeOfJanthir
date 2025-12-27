using System.Text.Json.Serialization;

namespace EyeOfJanthir.Models;

public sealed record DiscordEmbed(
    [property: JsonPropertyName("title")] string Title,
    [property: JsonPropertyName("description")] string Description,
    [property: JsonPropertyName("color")] int? Color);
