using AutoMapper;
using caso2_solucion.application.Interfaces;
using caso2_solucion.application.Proveedores.Dtos;
using MediatR;

namespace caso2_solucion.application.Proveedores.Queries.GetProveedorById
{
    public class GetSupplierByIdQueryHandler : IRequestHandler<GetSupplierByIdQuery, SupplierDto?>

    {
        private readonly ISupplierRepository _repository;
        private readonly IMapper _mapper;

        public GetSupplierByIdQueryHandler(ISupplierRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<SupplierDto?> Handle(GetSupplierByIdQuery request, CancellationToken cancellationToken)
        {
            var supplier = await _repository.GetByIdAsync(request.Id, cancellationToken);
            return supplier is null ? null : _mapper.Map<SupplierDto>(supplier);
        }

    }
}
