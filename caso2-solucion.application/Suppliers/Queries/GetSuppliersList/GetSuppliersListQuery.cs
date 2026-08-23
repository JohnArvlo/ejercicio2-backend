using caso2_solucion.application.Proveedores.Dtos;
using MediatR;

namespace caso2_solucion.application.Proveedores.Queries.GetProveedoresList
{
    public record GetSuppliersListQuery : IRequest<List<SupplierDto>>;

}
