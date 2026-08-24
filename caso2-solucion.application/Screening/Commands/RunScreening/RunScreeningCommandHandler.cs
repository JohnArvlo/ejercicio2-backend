using caso2_solucion.application.Common.Interfaces;
using caso2_solucion.application.Interfaces;
using caso2_solucion.application.Screening.Dtos;
using MediatR;

namespace caso2_solucion.application.Screening.Commands.RunScreening;

public class RunScreeningCommandHandler : IRequestHandler<RunScreeningCommand, ScreeningResultDto>
{
    private readonly ISupplierRepository _supplierRepository; 
    private readonly IScreeningApiClient _screeningApiClient;

    public RunScreeningCommandHandler(ISupplierRepository supplierRepository, IScreeningApiClient screeningApiClient)
    {
        _supplierRepository = supplierRepository;
        _screeningApiClient = screeningApiClient;
    }

    public async Task<ScreeningResultDto> Handle(RunScreeningCommand request, CancellationToken cancellationToken)
    {
        if (request.Sources.Count == 0 || request.Sources.Count > 3)
            throw new ArgumentException("Debes seleccionar entre 1 y 3 fuentes.");

        var supplier = await _supplierRepository.GetByIdAsync(request.SupplierId, cancellationToken)
            ?? throw new KeyNotFoundException($"Supplier con id {request.SupplierId} no encontrado.");

        
        return await _screeningApiClient.BuscarAsync(supplier.LegalName, request.Sources, cancellationToken);
    }
}