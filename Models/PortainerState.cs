using System.Collections.Immutable;

namespace EyeOfJanthir.Models;

public sealed class PortainerState
{
    public required ImmutableList<PortainerEndpoint> Endpoints { get; init; }
    public required ImmutableList<(PortainerEndpoint Endpoint, ImmutableList<ContainerInfo>? Containers)> Environments { get; init; }
}
