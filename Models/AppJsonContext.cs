using System.Text.Json.Serialization;

namespace EyeOfJanthir.Models;

[JsonSerializable(typeof(ContainerInfo))]
[JsonSerializable(typeof(List<ContainerInfo>))]
[JsonSerializable(typeof(ContainerInspect))]
[JsonSerializable(typeof(ContainerState))]
[JsonSerializable(typeof(HealthState))]
[JsonSerializable(typeof(PortainerEndpoint))]
[JsonSerializable(typeof(List<PortainerEndpoint>))]
[JsonSerializable(typeof(DiscordWebhookPayload))]
[JsonSerializable(typeof(DiscordEmbed))]
[JsonSourceGenerationOptions(PropertyNamingPolicy = JsonKnownNamingPolicy.CamelCase)]
public partial class AppJsonContext : JsonSerializerContext
{
}