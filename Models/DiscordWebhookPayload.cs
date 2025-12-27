using System.Text.Json.Serialization;

namespace EyeOfJanthir.Models;

public sealed record DiscordWebhookPayload(
    [property: JsonPropertyName("content")] string? Content,
    [property: JsonPropertyName("embeds")] List<DiscordEmbed>? Embeds);
