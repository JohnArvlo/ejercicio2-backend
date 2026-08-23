using caso2_solucion.application.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace caso2_solucion.application.Proveedores.Commands.UpdateProveedorCommand
{
    public class UpdateSupplierCommandHandler
    : IRequestHandler<UpdateSupplierCommand, bool>
    {
        private readonly ISupplierRepository _repository;

        public UpdateSupplierCommandHandler(ISupplierRepository repository)
        {
            _repository = repository;
        }

        public async Task<bool> Handle(
            UpdateSupplierCommand request,
            CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(request.LegalName))
                throw new ArgumentException(
                    "Legal name is required.");

            if (string.IsNullOrWhiteSpace(request.TaxId))
                throw new ArgumentException(
                    "Tax ID is required.");

            if (request.TaxId.Length != 11)
                throw new ArgumentException(
                    "Tax ID must contain 11 digits.");

            if (!request.TaxId.All(char.IsDigit))
                throw new ArgumentException(
                    "Tax ID must contain only digits.");

            var supplier = await _repository.GetByIdAsync(
                request.Id,
                cancellationToken);

            if (supplier is null)
                return false;

            supplier.Update(
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

            await _repository.UpdateAsync(
                supplier,
                cancellationToken);

            return true;
        }
    }
}
