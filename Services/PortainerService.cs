using EyeOfJanthir.Models;
using Microsoft.Extensions.Logging;
using System.Net.Http.Json;

namespace EyeOfJanthir.Services;

public sealed class PortainerService(
    IHttpClientFactory httpClientFactory,
    ILogger<PortainerService> logger)
    : IDisposable
{
    private readonly HttpClient httpClient = httpClientFactory.CreateClient(nameof(PortainerService));
    private readonly ILogger<PortainerService> logger = logger;

    public async Task<List<PortainerEndpoint>?> GetEndpointsAsync(CancellationToken cancellationToken)
    {
        try
        {
            return await this.httpClient.GetFromJsonAsync(
                "/api/endpoints",
                AppJsonContext.Default.ListPortainerEndpoint, cancellationToken) ?? [];
        }
        catch (Exception ex)
        {
            this.logger.LogError(ex, "Encountered exception while getting endpoints");
            return default;
        }
    }

    public async Task<List<ContainerInfo>?> GetContainersAsync(PortainerEndpoint portainerEndpoint, CancellationToken cancellationToken)
    {
        try
        {
            return await this.httpClient.GetFromJsonAsync(
                $"/api/endpoints/{portainerEndpoint.Id}/docker/containers/json?all={true}",
                AppJsonContext.Default.ListContainerInfo, cancellationToken) ?? [];
        }
        catch (Exception ex)
        {
            this.logger.LogError(ex, "Encountered exception while getting containers");
            return default;
        }
    }

    public void Dispose() => this.httpClient.Dispose();
}