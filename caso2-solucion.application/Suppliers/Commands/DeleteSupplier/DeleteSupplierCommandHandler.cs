using caso2_solucion.application.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace caso2_solucion.application.Proveedores.Commands.DeleteProveedor
{
    public class DeleteSupplierCommandHandler : IRequestHandler<DeleteSupplierCommand>
    {
        private readonly ISupplierRepository _repository;

        public DeleteSupplierCommandHandler(ISupplierRepository repository)
        {
            _repository = repository;
        }

        public async Task Handle(DeleteSupplierCommand request, CancellationToken cancellationToken)
        {
            var supplier = await _repository.GetByIdIncludingDeletedAsync(request.Id, cancellationToken)
                ?? throw new KeyNotFoundException($"Proveedor con id {request.Id} no encontrado.");

            await _repository.DeleteAsync(request.Id, cancellationToken);
        }
    }
}
