using caso2_solucion.domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace caso2_solucion.application.Suppliers.Commands.UpdateSupplier
{
    public record UpdateSupplierRequest(
        string LegalName,
        string TradeName,
        string TaxId,
        string PhoneNumber,
        string Email,
        string Website,
        string PhysicalAddress,
        ECountry Country,
        decimal AnnualRevenueUsd
    );
}
