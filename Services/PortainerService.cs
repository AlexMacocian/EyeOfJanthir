using EyeOfJanthir.Models;
using System.Net.Http.Json;

namespace EyeOfJanthir.Services;

public sealed class PortainerService
    : IDisposable
{
    private readonly HttpClient httpClient;

    public PortainerService(string portainerUrl, string apiToken)
    {
        this.httpClient = new HttpClient
        {
            BaseAddress = new Uri(portainerUrl),
            Timeout = TimeSpan.FromSeconds(30)
        };

        this.httpClient.DefaultRequestHeaders.Add("X-API-Key", apiToken);
    }

    public async Task<List<PortainerEndpoint>> GetEndpointsAsync(CancellationToken ct = default)
    {
        return await this.httpClient.GetFromJsonAsync(
            "/api/endpoints",
            PortainerJsonContext.Default.ListPortainerEndpoint, ct) ?? [];
    }

    public async Task<List<ContainerInfo>> GetContainersAsync(int endpointId, bool all = true, CancellationToken ct = default)
    {
        return await this.httpClient.GetFromJsonAsync(
            $"/api/endpoints/{endpointId}/docker/containers/json?all={all}",
            PortainerJsonContext.Default.ListContainerInfo, ct) ?? [];
    }

    public void Dispose() => this.httpClient.Dispose();
}