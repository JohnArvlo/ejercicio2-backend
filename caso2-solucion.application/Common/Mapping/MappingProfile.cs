using AutoMapper;
using caso2_solucion.domain.entities;
using caso2_solucion.application.Proveedores.Dtos;

namespace caso2_solucion.application.Common.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Supplier, SupplierDto>();
        }
    }
}