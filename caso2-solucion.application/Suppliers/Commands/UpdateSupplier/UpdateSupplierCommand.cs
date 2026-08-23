using caso2_solucion.domain.ValueObjects;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace caso2_solucion.application.Proveedores.Commands.UpdateProveedorCommand
{
    public record UpdateSupplierCommand(
        int Id,
        string LegalName,
        string TradeName,
        string TaxId,
        string PhoneNumber,
        string Email,
        string Website,
        string PhysicalAddress,
        ECountry Country,
        decimal AnnualRevenueUsd
    ) : IRequest<bool>;
}
