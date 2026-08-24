using MediatR;
using caso2_solucion.application.Screening.Dtos;

namespace caso2_solucion.application.Screening.Commands.RunScreening;

public record RunScreeningCommand(int SupplierId, List<string> Sources) : IRequest<ScreeningResultDto>;