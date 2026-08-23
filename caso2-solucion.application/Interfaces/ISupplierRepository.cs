using caso2_solucion.domain;

using caso2_solucion.domain.entities;

namespace caso2_solucion.application.Interfaces
{
    public interface ISupplierRepository
    {
        Task SoftDeleteAsync(Supplier supplier);
        Task<Supplier?> GetByIdAsync(int id, CancellationToken ct = default);
        Task<Supplier?> GetByIdIncludingDeletedAsync(int id, CancellationToken ct = default);
        Task<List<Supplier>> GetAllAsync(CancellationToken ct = default);
        Task<int> AddAsync(Supplier supplier, CancellationToken ct = default);
        Task UpdateAsync(Supplier supplier, CancellationToken ct = default);
        Task DeleteAsync(int id, CancellationToken ct = default);
    }
}
