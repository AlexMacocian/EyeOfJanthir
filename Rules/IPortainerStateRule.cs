using EyeOfJanthir.Models;

namespace EyeOfJanthir.Rules;

public interface IPortainerStateRule
{
    Task Execute(PortainerState currentState, PortainerState? previousState, CancellationToken cancellationToken);
}
