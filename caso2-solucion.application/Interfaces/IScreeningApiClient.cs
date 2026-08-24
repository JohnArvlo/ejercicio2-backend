using caso2_solucion.application.Screening.Dtos;

namespace caso2_solucion.application.Common.Interfaces;

public interface IScreeningApiClient
{
    Task<ScreeningResultDto> BuscarAsync(string entityName, List<string> sources, CancellationToken ct = default);
}