using System.Net.Http.Json;
using System.Text;
using caso2_solucion.application.Common.Interfaces;
using caso2_solucion.application.Screening.Dtos;
using Microsoft.Extensions.Configuration;

namespace caso2_solucion.infrastructure;

public class ScreeningApiClient : IScreeningApiClient
{
    private readonly HttpClient _httpClient;
    private readonly string _apiKey;

    public ScreeningApiClient(HttpClient httpClient, IConfiguration config)
    {
        _httpClient = httpClient;
        _apiKey = config["Screening:ApiKey"]
            ?? throw new InvalidOperationException("Falta configurar Screening:ApiKey");
    }

    public async Task<ScreeningResultDto> BuscarAsync(string entityName, List<string> sources, CancellationToken ct = default)
    {
        var query = new StringBuilder($"/api/screening?entity={Uri.EscapeDataString(entityName)}");
        foreach (var source in sources)
            query.Append($"&sources={Uri.EscapeDataString(source)}");

        using var request = new HttpRequestMessage(HttpMethod.Get, query.ToString());
        request.Headers.Add("X-API-Key", _apiKey);

        var response = await _httpClient.SendAsync(request, ct);
        response.EnsureSuccessStatusCode();

        var result = await response.Content.ReadFromJsonAsync<ScreeningResultDto>(cancellationToken: ct);
        return result ?? throw new InvalidOperationException("Respuesta vacía del API de screening.");
    }
}