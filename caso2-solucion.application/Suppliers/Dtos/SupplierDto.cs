namespace caso2_solucion.application.Proveedores.Dtos
{
    public record SupplierDto(
            int Id,
            string LegalName,
            string TradeName,
            string TaxId,
            string PhoneNumber,
            string Email,
            string Website,
            string PhysicalAddress,
            string Country,
            decimal AnnualRevenueUsd,
            DateTime LastModifiedAt
        );

}
