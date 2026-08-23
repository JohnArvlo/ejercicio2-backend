using caso2_solucion.domain.ValueObjects;

namespace caso2_solucion.domain.entities
{
    public class Supplier
    {
        public int Id { get; private set; }

        public string LegalName { get; private set; } = string.Empty;
        public string TradeName { get; private set; } = string.Empty;
        public string TaxId { get; private set; } = string.Empty;
        public string PhoneNumber { get; private set; } = string.Empty;
        public string Email { get; private set; } = string.Empty;
        public string Website { get; private set; } = string.Empty;
        public string PhysicalAddress { get; private set; } = string.Empty;

        public ECountry Country { get; private set; }

        public decimal AnnualRevenueUsd { get; private set; }

        public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;
        public DateTime LastModifiedAt { get; private set; } = DateTime.UtcNow;

        public bool IsDeleted { get; private set; }

        public Supplier(
            string legalName,
            string tradeName,
            string taxId,
            string phoneNumber,
            string email,
            string website,
            string physicalAddress,
            ECountry country,
            decimal annualRevenueUsd)
        {
            LegalName = legalName;
            TradeName = tradeName;
            TaxId = taxId;
            PhoneNumber = phoneNumber;
            Email = email;
            Website = website;
            PhysicalAddress = physicalAddress;
            Country = country;
            AnnualRevenueUsd = annualRevenueUsd;
        }

        public void Update(
            string legalName,
            string tradeName,
            string taxId,
            string phoneNumber,
            string email,
            string website,
            string physicalAddress,
            ECountry country,
            decimal annualRevenueUsd)
        {
            LegalName = legalName;
            TradeName = tradeName;
            TaxId = taxId;
            PhoneNumber = phoneNumber;
            Email = email;
            Website = website;
            PhysicalAddress = physicalAddress;
            Country = country;
            AnnualRevenueUsd = annualRevenueUsd;

            LastModifiedAt = DateTime.UtcNow;
        }

        public void Delete()
        {
            IsDeleted = true;
            LastModifiedAt = DateTime.UtcNow;
        }

        public void Restore()
        {
            IsDeleted = false;
            LastModifiedAt = DateTime.UtcNow;
        }
    }
}
