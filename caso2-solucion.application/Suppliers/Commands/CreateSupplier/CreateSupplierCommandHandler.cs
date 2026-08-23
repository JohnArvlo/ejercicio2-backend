using caso2_solucion.application.Interfaces;
using caso2_solucion.domain.entities;
using MediatR;

namespace caso2_solucion.application.Proveedores.Commands.CreateProveedor
{
    public class CreateSupplierCommandHandler : IRequestHandler<CreateSupplierCommand, int>
    {
        private readonly ISupplierRepository _repository;

        public CreateSupplierCommandHandler(ISupplierRepository repository)
        {
            _repository = repository;
        }

        public async Task<int> Handle(CreateSupplierCommand request, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(request.LegalName))
                throw new ArgumentException("La razón social es obligatoria.");

            if (request.TaxId.Length != 11)
                throw new ArgumentException("La identificación tributaria debe tener 11 dígitos.");

            var supplier = new Supplier(
                request.LegalName,
                request.TradeName,
                request.TaxId,
                request.PhoneNumber,
                request.Email,
                request.Website,
                request.PhysicalAddress,
                request.Country,
                request.AnnualRevenueUsd
            );

            return await _repository.AddAsync(supplier, cancellationToken);
        }
    }
}
