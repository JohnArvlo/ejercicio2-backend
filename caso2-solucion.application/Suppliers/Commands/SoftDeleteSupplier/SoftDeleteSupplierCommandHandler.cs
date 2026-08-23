using caso2_solucion.application.Interfaces;
using caso2_solucion.application.Proveedores.Commands.UpdateProveedorCommand;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace caso2_solucion.application.Proveedores.Commands.DeleteProveedor
{
    public class SoftDeleteSupplierCommandHandler : IRequestHandler<SoftDeleteSupplierCommand>
    {
        private readonly ISupplierRepository _repository;

        public SoftDeleteSupplierCommandHandler(ISupplierRepository repository)
        {
            _repository = repository;
        }

        public async Task Handle(SoftDeleteSupplierCommand request, CancellationToken cancellationToken)
        {
            var supplier = await _repository.GetByIdAsync(request.Id, cancellationToken)
                ?? throw new KeyNotFoundException($"Proveedor con id {request.Id} no encontrado.");

            await _repository.SoftDeleteAsync(supplier);
        }
    }
}
