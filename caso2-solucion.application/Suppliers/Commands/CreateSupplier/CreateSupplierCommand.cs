using caso2_solucion.domain.ValueObjects;
using MediatR;

namespace caso2_solucion.application.Proveedores.Commands.CreateProveedor;
    public record CreateSupplierCommand(
        string LegalName,
        string TradeName,
        string TaxId,
        string PhoneNumber,
        string Email,
        string Website,
        string PhysicalAddress,
        ECountry Country,
        decimal AnnualRevenueUsd

        ) : IRequest<int>;


