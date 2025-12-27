using System.Text.Json.Serialization;

namespace EyeOfJanthir.Models;

[JsonSerializable(typeof(ContainerInfo))]
[JsonSerializable(typeof(List<ContainerInfo>))]
[JsonSerializable(typeof(ContainerInspect))]
[JsonSerializable(typeof(ContainerState))]
[JsonSerializable(typeof(HealthState))]
[JsonSerializable(typeof(PortainerEndpoint))]
[JsonSerializable(typeof(List<PortainerEndpoint>))]
[JsonSourceGenerationOptions(PropertyNamingPolicy = JsonKnownNamingPolicy.CamelCase)]
public partial class PortainerJsonContext : JsonSerializerContext
{
}