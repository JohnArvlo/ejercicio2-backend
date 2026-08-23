using caso2_solucion.application.Proveedores.Dtos;
using MediatR;

namespace caso2_solucion.application.Proveedores.Queries.GetProveedorById
{
    public record GetSupplierByIdQuery(int Id) : IRequest<SupplierDto?>;
}
