using System.Text.Json;
using System.Text.Json.Serialization;

namespace caso2_solucion.application.Screening.Dtos;

public record ScreeningResultDto(
    [property: JsonPropertyName("entity_searched")] string EntitySearched,
    [property: JsonPropertyName("sources_queried")] List<string> SourcesQueried,
    [property: JsonPropertyName("total_hits")] int TotalHits,
    [property: JsonPropertyName("results")] List<ScreeningSourceResultDto> Results
);

public record ScreeningSourceResultDto(
    [property: JsonPropertyName("source")] string Source,
    [property: JsonPropertyName("hits")] int Hits,
    [property: JsonPropertyName("matches")] List<Dictionary<string, JsonElement>> Matches,
    [property: JsonPropertyName("error")] string? Error
);