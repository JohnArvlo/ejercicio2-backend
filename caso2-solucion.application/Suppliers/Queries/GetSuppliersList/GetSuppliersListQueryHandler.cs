using AutoMapper;
using caso2_solucion.application.Interfaces;
using caso2_solucion.application.Proveedores.Dtos;
using caso2_solucion.domain.entities;
using MediatR;

namespace caso2_solucion.application.Proveedores.Queries.GetProveedoresList
{
    public class GetSuppliersListQueryHandler : IRequestHandler<GetSuppliersListQuery, List<SupplierDto>>
    {
        private readonly ISupplierRepository _repository;
        private readonly IMapper _mapper;

        public GetSuppliersListQueryHandler(ISupplierRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<SupplierDto>> Handle(GetSuppliersListQuery request, CancellationToken cancellationToken)
        {
            var suppliers = await _repository.GetAllAsync(cancellationToken);

            return suppliers
                .OrderByDescending(p => p.LastModifiedAt)
                .Select(p => _mapper.Map<SupplierDto>(p)) 
                .ToList();
        }
    }
}
